using System;
using System.Threading.Tasks;
using Entities.Models.RequestModels;
using Entities.ResponseModels;

namespace Contracts
{
    public interface IAppUserService
    {
        Task<LoginResponse> CreateAsync(UserRegister model);
    }
}
