using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IAppUserRoleRepository : IRepositoryBase<AppUserRole>
    {
        IEnumerable<AppUserRole> GetAppUserRoleByUserId(Guid userId);

        void CreateAppUserRole(AppUserRole userRole);

        void UpdateAppUserRole(AppUserRole userRole);

        void DeleteAppUserRole(AppUserRole userRole);
    }
}