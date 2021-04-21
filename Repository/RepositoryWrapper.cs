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
        private IProductPhotoRepository _productPhoto;
        private ICategoryRepository _category;
        private IPaymentTypeRepository _paymentType;
        private IOrderStatusRepository _orderStatus;
        private IContactUsRepository _contactUs;
        private ISalesOrderRepository _salesOrder;

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

        public IProductPhotoRepository ProductPhoto
        {
            get
            {
                if (_productPhoto == null)
                {
                    _productPhoto = new ProductPhotoRepository(_context);
                }

                return _productPhoto;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_context);
                }

                return _category;
            }
        }

        public IPaymentTypeRepository PaymentType
        {
            get
            {
                if (_paymentType == null)
                {
                    _paymentType = new PaymentTypeRepository(_context);
                }

                return _paymentType;
            }
        }

        public IOrderStatusRepository OrderStatus
        {
            get
            {
                if (_orderStatus == null)
                {
                    _orderStatus = new OrderStatusRepository(_context);
                }

                return _orderStatus;
            }
        }

        public IContactUsRepository ContactUs
        {
            get
            {
                if (_contactUs == null)
                {
                    _contactUs = new ContacUsRepository(_context);
                }

                return _contactUs;
            }
        }

        public ISalesOrderRepository SalesOrder
        {
            get
            {
                if (_salesOrder == null)
                {
                    _salesOrder = new SalesOrderRepository(_context);
                }

                return _salesOrder;
            }
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        
    }
}
