using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrainReservation.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("Error")]    
    public class ErrorController : ControllerBase
    {
        [HttpGet, HttpPost]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var traceId = HttpContext.TraceIdentifier;

            // TODO: Log the detailed exception internally
            // _logger.LogError(exception, "Unhandled exception. TraceId: {TraceId}", traceId);

            var response = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                TraceId = traceId
            };

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
