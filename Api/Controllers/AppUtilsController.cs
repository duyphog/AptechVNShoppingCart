using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Models;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class AppUtilsController : ControllerBase
    {
        private readonly IAppUtilsService _appUtilsService;

        public AppUtilsController(IAppUtilsService appUtilsService)
        {
            _appUtilsService = appUtilsService;
        }

        [HttpGet("delivery-type")]
        public async Task<ActionResult> GetDeliveryTypeAsync()
        {
            var result = await _appUtilsService.FindAllDeliveryTypeAsync();
            if (!result.Succeed)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));

            return result.Value.Any() ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Warning", "Not exist value"));
        }

        [HttpGet("order-status")]
        public async Task<ActionResult> GetOrderStatusAsync()
        {
            var result = await _appUtilsService.FindAllOrderStatusAsync();
            if (!result.Succeed)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));

            return result.Value.Any() ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Warning", "Not exist value"));
        }

        [HttpGet("payment-type")]
        public async Task<ActionResult> GetPaymentTypeAsync()
        {
            var result = await _appUtilsService.FindAllPaymentTypeAsync();
            if (!result.Succeed)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));

            return result.Value.Any() ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Warning", "Not exist value"));
        }
    }
}
