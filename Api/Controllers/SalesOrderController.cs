using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Extentions;
using Api.Models;
using Contracts;
using Entities.Helpers;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;

        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> FindOrderAsync([FromForm] SalesOrderParameters parameters)
        {
            var result = await _salesOrderService.FindAllSalesOrderForCurrentUser(parameters);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            var list = result.Value;

            if (list.TotalCount == 0)
                return NoContent();

            Response.AddPagination(list.TotalCount, list.PageSize, list.CurrentPage, list.TotalPages, list.HasPrevious, list.HasNext);

            return Ok(list);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> FindOrderByAdminAsync([FromForm] SalesOrderParameters parameters)
        {
            var result = await _salesOrderService.FindAllSalesOrderAsync(parameters);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            var list = result.Value;

            if (list.TotalCount == 0)
                return NoContent();

            Response.AddPagination(list.TotalCount, list.PageSize, list.CurrentPage, list.TotalPages, list.HasPrevious, list.HasNext);

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(SalesOrderForCreate model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _salesOrderService.CreateAsync(model);
            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Create fail", result.Errors));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderAsync(string id, int orderStatus)
        {
            var param = new JObject
            {
                { "id", id },
                { "orderStatus", orderStatus }
            };

            var result = await _salesOrderService.UpdateOrderStatus(param);
            return result.Succeed ? Ok() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));
        }

        [HttpPost("payment")]
        public async Task<IActionResult> CreatePaymentForOrderAsync(PaymentDetailForCreate model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _salesOrderService.PaymentSalesOrderAsync(model);
            return result.Succeed ? Ok() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Create fail", result.Errors));
        }

        [Authorize(Policy = "RequireMember")]
        [HttpGet("my-orders")]
        public async Task<ActionResult<IEnumerable<SalesOrderDTO>>> GetOrderForUserAsync([FromForm] SalesOrderParameters parameters)
        {
            var result = await _salesOrderService.FindAllSalesOrderForCurrentUser(parameters);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            var list = result.Value;

            if (list.TotalCount == 0)
                return NoContent();

            Response.AddPagination(list.TotalCount, list.PageSize, list.CurrentPage, list.TotalPages, list.HasPrevious, list.HasNext);

            return Ok(list);
        }

        [Authorize(Policy = "RequireMember")]
        [HttpPost("trade-return")]
        public async Task<ActionResult<SalesOrderDTO>> TradeOrReturnForUserAsync(SaleOrderForTradeOrReturn model)
        {
            var result = await _salesOrderService.TradeOrReturnAsync(model);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
        }

    }
}
