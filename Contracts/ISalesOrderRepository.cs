using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ISalesOrderRepository : IRepositoryBase<SalesOrder>
    {
        Task AddRangeSalesOrderAsync(IEnumerable<SalesOrder> salesOrders);
    }
}
