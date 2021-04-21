using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Models;
using Contracts;
using Entities.Models.DataTransferObjects;
using Entities.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
            var result = await _appUserService.RegisterAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Register fail", result.Errors));

            return Ok(result.Value);
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
        public async Task<ActionResult<LoginResponse>> LoginAsync(JObject model)
        {
            var result = await _appUserService.LoginAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Login fail", result.Errors));

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDTO>> GetUsersDetailAsync(Guid id)
        {
            var result = await _appUserService.FindUserById(id);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));

            return Ok(result.Value);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetUsersAsync()
        {
            var result = await _appUserService.GetUsersAsync();

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));

            return Ok(result.Value);
        }

        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePasswordAsync(JObject model)
        {
            var result = await _appUserService.ChangePasswordAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Chage password fail", result.Errors));

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppUserDTO>> UpdateCurrentUserAsync(Guid id, AppUserForUpdate model)
        {
            if (!id.Equals(model.Id))
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Invalid id"));

            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Invalid model", ModelState.Values.SelectMany(e => e.Errors)));

            var result = await _appUserService.UpdateCurrentUserAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));

            return Ok(result.Value);
        }
    } 
}
