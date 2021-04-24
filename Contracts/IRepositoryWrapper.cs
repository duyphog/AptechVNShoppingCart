using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IAppUserRepository AppUser { get; }
        IAppUserRoleRepository AppUserRole { get; }
        IAppRoleRepository AppRole { get; }
        IProductRepository Product { get; }
        IProductPhotoRepository ProductPhoto { get; }
        ICategoryRepository Category { get; }
        IPaymentTypeRepository PaymentType { get; }
        IOrderStatusRepository OrderStatus { get; }
        IContactUsRepository ContactUs { get; }
        ISalesOrderRepository SalesOrder { get; }
        IDeliveryTypeRepository DeliveryType { get; }

        Task<int> SaveAsync();
    }
}
