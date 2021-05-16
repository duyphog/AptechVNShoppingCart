using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class TradeReturnRequest
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public string SalesOrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int RequestStatus { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Product Product { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
    }
}
