using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductPhotos = new HashSet<ProductPhoto>();
            SalesOrders = new HashSet<SalesOrder>();
            TradeReturnRequests = new HashSet<TradeReturnRequest>();
        }

        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int? Stock { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public string CategoryId { get; set; }
        public bool? Unlimited { get; set; }
        public string Location { get; set; }
        public int? WarrantyPeriod { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<TradeReturnRequest> TradeReturnRequests { get; set; }
    }
}
