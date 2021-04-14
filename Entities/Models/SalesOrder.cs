using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            Deliveries = new HashSet<Delivery>();
            PaidDetails = new HashSet<PaidDetail>();
            ProductReturnRequests = new HashSet<ProductReturnRequest>();
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public Guid AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Contry { get; set; }
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string OrderNote { get; set; }
        public int OrderStatusId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<PaidDetail> PaidDetails { get; set; }
        public virtual ICollection<ProductReturnRequest> ProductReturnRequests { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
