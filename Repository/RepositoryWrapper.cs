using System;
using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;

        private IAppUserRepository _appUser;

        private IAppUserRoleRepository _appUserRole;

        private IProductRepository _product;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IAppUserRepository AppUser
        {
            get
            {
                if (_appUser == null)
                {
                    _appUser = new AppUserRepository(_repoContext);
                }

                return _appUser;
            }
        }

        public IAppUserRoleRepository AppUserRole
        {
            get
            {
                if (_appUserRole == null)
                {
                    _appUserRole = new AppUserRoleRepository(_repoContext);
                }

                return _appUserRole;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_repoContext);
                }

                return _product;
            }
        }

        public async Task SaveAsync() => await _repoContext.SaveChangesAsync();
        
    }
}
