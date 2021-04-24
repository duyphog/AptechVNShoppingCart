using System;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Models.DataTransferObjects;

namespace Contracts
{
    public interface ISalesOrderService
    {
        Task<ProcessResult> CreateAsync(SalesOrderForCreate salesOrder);
    }
}
