using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int Id { get; set; }
        public string OrderStatusName { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }

        public static implicit operator OrderStatus(int v)
        {
            throw new NotImplementedException();
        }
    }
}
