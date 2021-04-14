using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ProductReturnRequest
    {
        public ProductReturnRequest()
        {
            ProductReturnRequestDetails = new HashSet<ProductReturnRequestDetail>();
        }

        public Guid Id { get; set; }
        public Guid? AppUserId { get; set; }
        public Guid? SalesOrderId { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int RequestStatus { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
        public virtual ICollection<ProductReturnRequestDetail> ProductReturnRequestDetails { get; set; }
    }
}
