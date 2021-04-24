using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IDeliveryTypeRepository : IRepositoryBase<DeliveryType>
    {
        Task<IEnumerable<DeliveryType>> FindAllDeliveryTypeAsync();
    }
}
