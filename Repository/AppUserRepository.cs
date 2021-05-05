using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }

        public Task<PagedList<AppUser>> FindAllAppUserAsync(AppUserParameters parameters)
        {
            var queries = FindAll().Include(u => u.AppUserRoles).ThenInclude(ur => ur.Role).AsQueryable();

            //if (parameters.StatusType != null)
            //{
            //    queries = queries.Where(u => u.Status == parameters.StatusType);
            //}

            if (parameters.RoleID != null)
            {
                queries = queries.Where(u => u.AppUserRoles.Any(x=> x.RoleId == parameters.RoleID));
            }

            queries = queries.OrderByDescending(x => x.CreateDate);

            return PagedList<AppUser>.ToPagedList(queries, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<AppUser> FindAppUserByIdAsync(Guid id)
        {
            return await FindByCondition(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AppUser> FindAppUserByUserNameAsync(string userName)
        {
            return await FindByCondition(u => u.UserName == userName).Include(x=>x.AppUserRoles).ThenInclude(x=>x.Role).FirstOrDefaultAsync();
        }
    }
}
