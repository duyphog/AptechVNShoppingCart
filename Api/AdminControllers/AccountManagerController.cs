using System;
using System.Net;
using System.Threading.Tasks;
using Api.Models;
using Contracts;
using Entities.Models.DataTransferObjects;
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
        public async Task<ActionResult<AppUserDTO>> RegisterByAdminAsync(AppUserForRegister model)
        {
            var result = await _appUserService.CreateAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Register fail", result.Errors));

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            var result = await _appUserService.DeleteAsync(id);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppUserDTO>> UpdateUserAsync(Guid id, AppUserForUpdate model)
        {
            if(model.Id != id)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", "Id Invalid"));
            }
            var result = await _appUserService.UpdateAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));

            return Ok(result.Value);
        }

    }
}
