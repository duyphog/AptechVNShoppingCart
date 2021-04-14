using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ProductReturnRequestDetail
    {
        public Guid Id { get; set; }
        public int Record { get; set; }
        public Guid? RequestId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductReturnRequest Request { get; set; }
    }
}
