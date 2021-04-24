using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class DeliveryType
    {
        public DeliveryType()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
