using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Models.DataTransferObjects;

namespace Contracts
{
    public interface IAppUtilsService
    {
        Task<ProcessResult<IEnumerable<PaymentTypeDTO>>> FindAllPaymentTypeAsync();
        Task<ProcessResult<IEnumerable<OrderStatusDTO>>> FindAllOrderStatusAsync();
        Task<ProcessResult<IEnumerable<DeliveryTypeDTO>>> FindAllDeliveryTypeAsync();
        Task<ProcessResult<IEnumerable<AppRoleDTO>>> FindAllAppRoleAsync();
    }
}
