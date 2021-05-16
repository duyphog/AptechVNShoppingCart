using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Entities.ResponseModels;
using Newtonsoft.Json.Linq;

namespace Contracts
{
    public interface IAppUserService
    {
        Task<ProcessResult<PagedList<AppUserDTO>>> GetUsersAsync(AppUserParameters parameters);
        Task<ProcessResult<AppUserDTO>> FindUserByIdAsync(Guid id);
        Task<ProcessResult<LoginResponse>> LoginAsync(UserLogin model);
        Task<ProcessResult<LoginResponse>> RegisterAsync(AppUserForRegister user);
        Task<ProcessResult<AppUserDTO>> CreateAsync(AppUserForRegister user);
        Task<ProcessResult<AppUserDTO>> UpdateAsync(AppUserForUpdate user);
        Task<ProcessResult<AppUserDTO>> UpdateCurrentUserAsync(AppUserForUpdate user);
        Task<ProcessResult> ChangePasswordAsync(ChangePassword model);
        Task<ProcessResult> DeleteAsync(Guid id);
        ProcessResult<AppUserDTO> GetProfileAsync();
    }
}
