using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Entities.ResponseModels;
using Newtonsoft.Json.Linq;

namespace Contracts
{
    public interface IAppUserService
    {
        Task<ProcessResult<IEnumerable<AppUserDTO>>> GetUsersAsync();

        Task<ProcessResult<AppUserDTO>> FindUserById(Guid id);

        Task<ProcessResult<LoginResponse>> LoginAsync(JObject model);

        Task<ProcessResult<LoginResponse>> RegisterAsync(AppUserForRegister user);

        Task<ProcessResult<AppUserDTO>> CreateAsync(AppUserForRegister user);

        Task<ProcessResult<AppUserDTO>> UpdateAsync(AppUserForUpdate user);

        Task<ProcessResult<AppUserDTO>> UpdateCurrentUserAsync(AppUserForUpdate user);

        Task<ProcessResult> ChangePasswordAsync(JObject model);

        Task<ProcessResult> DeleteAsync(Guid id);
    }
}
