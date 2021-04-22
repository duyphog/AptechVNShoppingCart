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
        Task<PagedList<AppUser>> FindAllAppUserAsync(AppUserParameters parameters);
    }
}
