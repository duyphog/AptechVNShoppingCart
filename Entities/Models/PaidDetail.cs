using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class PaidDetail
    {
        public Guid Id { get; set; }
        public Guid? SalesOrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? PaidDate { get; set; }
        public string FullName { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }
    }
}
