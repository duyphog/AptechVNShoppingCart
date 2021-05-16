﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Newtonsoft.Json.Linq;

namespace Contracts
{
    public interface ISalesOrderService
    {
        Task<ProcessResult<JObject>> CreateAsync(SalesOrderForCreate salesOrder);
        Task<ProcessResult> UpdateOrderStatus(JObject param);
        Task<ProcessResult<PagedList<SalesOrderDTO>>> FindAllSalesOrderAsync(SalesOrderParameters parameters);
        Task<ProcessResult<PagedList<SalesOrderDTO>>> FindAllSalesOrderByCurrentUser(SalesOrderParameters parameters);
        Task<ProcessResult> PaymentSalesOrder(PaymentDetailForCreate model);
    }
}
