using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IAppRoleRepository : IRepositoryBase<AppRole>
    {
        Task<AppRole> FindAppRoleById(Guid id);

        Task<AppRole> FindAppRoleByName(string roleName);

        void CreateAppRole(AppRole user);
                   
        void UpdateAppRole(AppRole user);
                   
        void DeleteAppRole(AppRole user);
    }
}
