﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AppRoleRepository : RepositoryBase<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(Entities.ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<AppRole> FindAppRoleById(Guid id)
        {
            return await FindByCondition(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AppRole> FindAppRoleByName(string roleName)
        {
            return await FindByCondition(r => r.Name == roleName).FirstOrDefaultAsync();
        }

        public void CreateAppRole(AppRole user)
        {
            Create(user);
        }

        public void UpdateAppRole(AppRole user)
        {
            Update(user);
        }

        public void DeleteAppRole(AppRole user)
        {
            Delete(user);
        }
    }
}
