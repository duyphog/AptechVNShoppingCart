using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Models;
using Contracts;
using Entities.Models.DTOs;
using Entities.Models.RequestModels;
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

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetUsersAsync()
        {
            var result = await _appUserService.GetUsersAsync();

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, string.Join(", ", result.Errors.ToArray())));

            return Ok(new AppResponse<IEnumerable<AppUserDTO>>(result.Value));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> RegisterAsync(UserRegister model)
        {
            var result = await _appUserService.RegisterAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, string.Join(", ", result.Errors.ToArray())));

            return Ok(new AppResponse<LoginResponse>(result.Value));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync(JObject model)
        {
            var result = await _appUserService.LoginAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, string.Join(", ", result.Errors.ToArray())));

            return Ok(new AppResponse<LoginResponse>(result.Value));
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePasswordAsync(JObject model)
        {
            var result = await _appUserService.ChangePasswordAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, string.Join(", ", result.Errors.ToArray())));

            return Ok();
        }
    } 
}
