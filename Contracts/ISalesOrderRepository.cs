using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;

namespace Contracts
{
    public interface ISalesOrderRepository : IRepositoryBase<SalesOrder>
    {
        Task AddRangeSalesOrderAsync(IEnumerable<SalesOrder> salesOrders);
        Task<SalesOrder> FindSalesOrderByIdAsync(string id);
        Task<PagedList<SalesOrder>> FindSalesOrderAsync(SalesOrderParameters salesOrderParameters);
    }
}
