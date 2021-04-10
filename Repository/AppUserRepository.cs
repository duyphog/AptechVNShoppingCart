using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        public AppUserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public AppUser GetAppUserById(Guid id)
        {
            return FindAll().FirstOrDefault(u => u.Id == id);
        }

        public AppUser GetAppUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public PagedList<AppUser> GetAppUsers(ProductParameters productParameters)
        {
            throw new NotImplementedException();
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
