using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            PaymentDetails = new HashSet<PaymentDetail>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int Id { get; set; }
        public string PaymentTypeName { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsPaid { get; set; }

        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
