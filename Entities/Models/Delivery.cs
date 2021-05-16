using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Delivery
    {
        public Guid Id { get; set; }
        public string SalesOrderId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public decimal? SalesOrderAmount { get; set; }
        public decimal? FeeAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryStatus { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }
    }
}
