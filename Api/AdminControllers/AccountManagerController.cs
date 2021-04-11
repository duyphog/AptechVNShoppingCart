using System;
using System.Net;
using System.Threading.Tasks;
using Api.Models;
using Contracts;
using Entities.Models.DTOs;
using Entities.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.AdminControllers
{
    public class AccountManagerController : AdminControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AccountManagerController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost]
        public async Task<ActionResult<AppUserDTO>> RegisterByAdminAsync(UserRegister model)
        {
            var result = await _appUserService.CreateAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, string.Join(", ", result.Errors.ToArray())));

            return Ok(new AppResponse<AppUserDTO>(result.Value));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            var result = await _appUserService.DeleteAsync(id);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, string.Join(", ", result.Errors.ToArray())));

            return Ok();
        }
    }
}
