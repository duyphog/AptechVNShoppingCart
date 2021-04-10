using System;
using System.Threading.Tasks;
using Api.Models;
using Contracts;
using Entities.Models.RequestModels;
using Entities.ResponseModels;
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

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> RegisterAsync(UserRegister model)
        {
            var result = await _appUserService.CreateAsync(model);

            return Ok(new AppResponse<LoginResponse>(result));
        }
    } 
}
