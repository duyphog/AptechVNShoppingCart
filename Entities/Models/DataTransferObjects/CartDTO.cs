using System;
using System.Collections.Generic;

namespace Entities.Models.DataTransferObjects
{
    public class CartDTO
    {
        public SalesOrderDTO SalesOrder { get; set; }
        public ICollection<SalesOrderDetail> Details { get; set; }
    }
}
