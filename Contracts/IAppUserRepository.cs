using System;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;

namespace Contracts
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {      
        Task<AppUser> FindAppUserByIdAsync(Guid id);

        Task<AppUser> FindAppUserByUserNameAsync(string userName);

        void CreateAppUser(AppUser user);

        void UpdateAppUser(AppUser user);

        void DeleteAppUser(AppUser user);
    }
}
