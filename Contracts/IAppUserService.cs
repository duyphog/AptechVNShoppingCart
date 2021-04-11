using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.RequestModels;
using Entities.ResponseModels;
using Newtonsoft.Json.Linq;

namespace Contracts
{
    public interface IAppUserService
    {
        Task<ProcessResult<IEnumerable<AppUserDTO>>> GetUsersAsync();

        Task<ProcessResult<AppUserDTO>> FindUserById(Guid id);

        Task<ProcessResult<LoginResponse>> LoginAsync(JObject model);

        Task<ProcessResult<LoginResponse>> RegisterAsync(UserRegister model);

        Task<ProcessResult<AppUserDTO>> CreateAsync(UserRegister model);

        Task<ProcessResult<AppUserDTO>> UpdateAsync(UserUpdate model);

        Task<ProcessResult> ChangePasswordAsync(JObject model);

        Task<ProcessResult> DeleteAsync(Guid id);
    }
}
