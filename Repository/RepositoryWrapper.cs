using System;
using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ShoppingCartContext _context;

        private IAppUserRepository _appUser;

        private IAppRoleRepository _appRole;

        private IAppUserRoleRepository _appUserRole;

        private IProductRepository _product;

        public RepositoryWrapper(ShoppingCartContext context)
        {
            _context = context;
        }

        public IAppUserRepository AppUser
        {
            get
            {
                if (_appUser == null)
                {
                    _appUser = new AppUserRepository(_context);
                }

                return _appUser;
            }
        }

        public IAppRoleRepository AppRole
        {
            get
            {
                if (_appRole == null)
                {
                    _appRole = new AppRoleRepository(_context);
                }

                return _appRole;
            }
        }

        public IAppUserRoleRepository AppUserRole
        {
            get
            {
                if (_appUserRole == null)
                {
                    _appUserRole = new AppUserRoleRepository(_context);
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
                    _product = new ProductRepository(_context);
                }

                return _product;
            }
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
        
    }
}
