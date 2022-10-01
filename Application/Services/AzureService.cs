using Application.Common.Exceptions;
using Application.Common.Extenstions;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AzureServices
    {
        private BlobServiceClient _blobServiceClient { get; set; }

        private readonly ILogger<AzureServices> _logger;

        public AzureServices(
            BlobServiceClient blobServiceClient,
            ILogger<AzureServices> logger
            )
        {
            _blobServiceClient = blobServiceClient;
            _logger = logger;
        }

        public async Task<BlobContainerClient> CreateBlobContainerClient(string containerName)
        {
            return await _blobServiceClient.CreateBlobContainerAsync(containerName);
        }

        public BlobContainerClient GetBlobContainerClient(string containerName)
        {
            _logger.LogInformation($"Method:GetBlobContainerClient -> containerName: {containerName}");

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if (!containerClient.Exists())
            {
                _logger.LogError("Method:UploadFileBlob Error");
            }
            return containerClient;
        }

        public IEnumerable<string> GetBlobs(string containerName, string searchPattern = "")
        {
            _logger.LogInformation($"Method:GetBlobs -> containerName: {containerName}, searchPattern: {searchPattern}");

            var containerClient = GetBlobContainerClient(containerName);
            var blobs = containerClient.GetBlobs();
            Regex regex = new Regex(searchPattern);

            return blobs.Select(x => x.Name).Where(name => regex.IsMatch(name));
        }

        public async Task UploadFileBlob(string containerName, string name, Stream stream)
        {
            try
            {
                _logger.LogInformation($"Method: UploadFileBlob -> containerName: {containerName}, name: {name}");

                var containerClient = GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(name);
                stream.Position = 0;

                await blobClient.UploadAsync(stream);
            }
            catch (Exception e)
            {
                _logger.LogError("Method: UploadFileBlob Error", e);
            }
        }

        public async Task UploadTextBlob(string containerName, string name, string text)
        {
            try
            {
                _logger.LogInformation($"Method: UploadFileBlob -> containerName: {containerName}, name: {name}");

                var containerClient = GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(name);

                await blobClient.UploadText(text);
            }
            catch (Exception e)
            {
                _logger.LogError("Method: UploadTextBlob Error", e);
            }
        }

        public async Task<Stream> DownloadFileBlob(string containerName, string name)
        {
            try
            {
                _logger.LogInformation($"Method: DownloadFileBlob -> containerName: {containerName}, name: {name}");

                var containerClient = GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(name);
                using (var stream = new MemoryStream())
                {
                    await blobClient.DownloadToAsync(stream);
                    return stream;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Method: DownloadFileBlob Error", e);
                return Stream.Null;
            }
        }

        public async Task<string> ReadTextBlob(string containerName, string enc, string name)
        {
            var text = "";

            try
            {
                _logger.LogInformation($"Method: ReadTextBlob -> containerName: {containerName}, encoding: {enc}, name: {name}");

                var containerClient = GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(name);
                using (var stream = new MemoryStream())
                {
                    await blobClient.DownloadToAsync(stream);
                    text = Encoding.GetEncoding(enc).GetString(stream.ToArray());
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Method: ReadTextBlob Error", e);
            }

            return text;
        }

        public async Task RemoveBlob(string containerName, string name)
        {
            var containerClient = GetBlobContainerClient(containerName);
            await containerClient.DeleteBlobIfExistsAsync(name);
        }

    }
}
