using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AppUserRoleRepository : RepositoryBase<AppUserRole>, IAppUserRoleRepository
    {
        public AppUserRoleRepository(ShoppingCartContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AppUserRole>> GetRolesByUserIdAsync(Guid userId)
        {
            return await FindByCondition(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<AppRole>> GetRolesByUserId(Guid userId)
        {
            return await FindAll().Include(x => x.Role).Where(x => x.UserId.Equals(userId))
                    .Select(x => x.Role).ToListAsync();
        }

        public void CreateAppUserRole(AppUserRole userRole)
        {
            Create(userRole);
        }

        public void UpdateAppUserRole(AppUserRole userRole)
        {
            Update(userRole);
        }

        public void DeleteAppUserRole(AppUserRole userRole)
        {
            Delete(userRole);
        }
    }
}
