using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AzureServices _azureServices;
        private readonly ILogger<UploadController> _logger;

        public UploadController(
            IConfiguration configuration, 
            AzureServices azureServices, 
            ILogger<UploadController> logger
            )
        {
            _configuration = configuration;
            _azureServices = azureServices;
            _logger = logger;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> FileUpload()
        {
            try
            {
                var files = this.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                            await _azureServices.UploadFileBlob(_configuration.GetValue<string>("Azure:ContainerName"), file.FileName, stream);
                        }
                    }
                }
                return Ok(new { count = files.Count() });
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Fileupload error", ex);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listblobs")]
        public ActionResult GetLobs(string searchPattern)
        {
            return Ok(_azureServices.GetBlobs(_configuration.GetValue<string>("Azure:ContainerName"), searchPattern));
        }
    }
}
