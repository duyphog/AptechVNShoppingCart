using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class PaymentDetail
    {
        public Guid Id { get; set; }
        public int? PaymentTypeId { get; set; }
        public string OrderNumber { get; set; }
        public string CartNumber { get; set; }
        public string CartHolderName { get; set; }
        public DateTime ValidUntil { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public bool? IsSuccess { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual PaymentType PaymentType { get; set; }
    }
}
