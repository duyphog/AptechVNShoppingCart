using System;
using System.Collections.Generic;

namespace Entities.Models.DataTransferObjects
{
    public class CartViewModel
    {
        public SalesOrder SalesOrder { get; set; }
        public ICollection<SalesOrderDetail> Details { get; set; }
    }
}
