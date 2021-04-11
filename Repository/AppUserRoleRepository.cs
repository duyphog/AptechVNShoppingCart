using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AppUserRoleRepository : RepositoryBase<AppUserRole>, IAppUserRoleRepository
    {
        public AppUserRoleRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AppUserRole>> GetAppUserRoleByUserIdAsync(Guid userId)
        {
            return await FindByCondition(x => x.UserId == userId).ToListAsync();
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
