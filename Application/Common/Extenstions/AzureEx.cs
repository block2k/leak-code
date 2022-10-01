using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extenstions
{
    public static class AzureEx
    {
        public static async Task UploadText(this BlobClient blob, string text)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                await blob.UploadAsync(ms);
            }
        }
    }
}
