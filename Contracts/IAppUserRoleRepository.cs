using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IAppUserRoleRepository : IRepositoryBase<AppUserRole>
    {
        Task<IEnumerable<AppUserRole>> GetAppUserRoleByUserIdAsync(Guid userId);

        void CreateAppUserRole(AppUserRole userRole);

        void UpdateAppUserRole(AppUserRole userRole);

        void DeleteAppUserRole(AppUserRole userRole);
    }
}