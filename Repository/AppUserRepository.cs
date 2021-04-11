using System;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        public AppUserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<AppUser> FindAppUserByIdAsync(Guid id)
        {
            return await FindAll().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser> FindAppUserByUserNameAsync(string userName)
        {
            return await FindAll().FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public void CreateAppUser(AppUser user)
        {
            Create(user);
        }

        public void UpdateAppUser(AppUser user)
        {
            Update(user);
        }

        public void DeleteAppUser(AppUser user)
        {
            Delete(user);
        }
    }
}
