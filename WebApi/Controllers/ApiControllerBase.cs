using Microsoft.AspNetCore.Mvc;
using NextFap.Application.Common.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        public ApiResponse? _apiResponse { get; set; }
        // Handler JWT, Identity
    }
}
