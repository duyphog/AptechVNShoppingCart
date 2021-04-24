using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IPaymentTypeRepository : IRepositoryBase<PaymentType>
    {
        Task<IEnumerable<PaymentType>> FindAllOrderStatusAsync();
    }
}
