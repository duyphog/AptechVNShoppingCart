using System;
using System.Collections.Generic;
using Entities.Models;
using Entities.Models.DataTransferObjects;

namespace Contracts
{
    public interface IGetDataService
    {
        IEnumerable<PaymentTypeDTO> GetPaymentType();
        IEnumerable<OrderStatusDTO> GetOrderStatus();
    }
}
