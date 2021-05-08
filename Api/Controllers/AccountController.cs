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
using Entities.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> RegisterAsync(AppUserForRegister model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _appUserService.RegisterAsync(model);
            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Register fail", result.Errors));
        }

        [AllowAnonymous]
        [HttpGet("genders")]
        public ActionResult<IEnumerable<Gender>> GetGender()
        {
            var genders = new List<Gender>
            {
                new Gender { Id = -1, Value= "UnKnown"},
                new Gender { Id = 0, Value= "Male"},
                new Gender { Id = 1, Value= "Female"},
            };
            return Ok(genders);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync(UserLogin model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _appUserService.LoginAsync(model);

            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Login fail", result.Errors));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDTO>> GetUsersDetailAsync(Guid id)
        {
            var result = await _appUserService.FindUserByIdAsync(id);
            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
        }

        [Authorize(Policy = "RequireAdminRole")]
        //[AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetUsersAsync([FromForm] AppUserParameters parameters)
        {
            var result = await _appUserService.GetUsersAsync(parameters);
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

        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePasswordAsync(ChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _appUserService.ChangePasswordAsync(model);

            return result.Succeed ? NoContent() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Chage password fail", result.Errors));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppUserDTO>> UpdateCurrentUserAsync(Guid id, AppUserForUpdate model)
        {
            if (!id.Equals(model.Id))
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Invalid id"));

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _appUserService.UpdateCurrentUserAsync(model);
            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));
        }
    } 
}
