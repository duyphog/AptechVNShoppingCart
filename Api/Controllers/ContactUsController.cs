using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.AppServices;
using Api.Extentions;
using Api.Models;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    public class ContactUsController : ControllerBase
    {
        private readonly ContactUsService _contactUsService;

        public ContactUsController(ContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactUs>>> GetAllAsync([FromForm] ContactUsParameters parameters)
        {
            var result = await _contactUsService.FindAllContacUsAsync(parameters);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            var list = result.Value;

            if (list.TotalCount == 0)
                return NoContent();

            Response.AddPagination(list.TotalCount, list.PageSize, list.CurrentPages, list.TotalPages, list.HasPrevious, list.HasNext);

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactUs>> GetByIdAsync(Guid id)
        {
            var result = await _contactUsService.FindContacUsByIdAsync(id);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            return result.Value != null ? Ok(result.Value) : NotFound(new ErrorResponse(HttpStatusCode.BadRequest, "Warning", "Not exist value"));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(ContactUsForCreate model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _contactUsService.CreateContacUsAsync(model);
            return result.Succeed ? NoContent() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Create fail", result.Errors));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContactUs>> UpdateAsync(Guid id, ContactUsForUpdate model)
        {
            if (id != model.Id)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Id Invalid"));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _contactUsService.UpdateContacUsAsync(model);
            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));
        }
    }
}
