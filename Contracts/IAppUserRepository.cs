using System;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;

namespace Contracts
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {
        PagedList<AppUser> GetAppUsers(ProductParameters productParameters);

        AppUser GetAppUserById(Guid id);

        AppUser GetAppUserByUserName(string userName);

        void CreateAppUser(AppUser user);

        void UpdateAppUser(AppUser user);

        void DeleteAppUser(AppUser user);
    }
}
