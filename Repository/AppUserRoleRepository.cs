using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class AppUserRoleRepository : RepositoryBase<AppUserRole>, IAppUserRoleRepository
    {
        public AppUserRoleRepository(RepositoryContext context) : base(context)
        {
        }

        public IEnumerable<AppUserRole> GetAppUserRoleByUserId(Guid userId)
        {
            return FindByCondition(x => x.UserId == userId);
        }

        public void CreateAppUserRole(AppUserRole userRole)
        {
            throw new NotImplementedException();
        }

        public void UpdateAppUserRole(AppUserRole userRole)
        {
            throw new NotImplementedException();
        }

        public void DeleteAppUserRole(AppUserRole userRole)
        {
            throw new NotImplementedException();
        }
    }
}
