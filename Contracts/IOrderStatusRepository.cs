using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IOrderStatusRepository : IRepositoryBase<OrderStatus>
    {
        Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync();
    }
}
