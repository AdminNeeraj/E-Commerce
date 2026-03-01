using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[ApiController]
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)] // to hide this controller from swagger documentation
    public class ErrorController : BaseApiController
    {
        [HttpGet]
        public ActionResult GetError(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
        
    }
}