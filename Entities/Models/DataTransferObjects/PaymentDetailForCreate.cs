using System;
namespace Entities.Models.DataTransferObjects
{
    public class PaymentDetailForCreate
    {
        public int PaymentTypeId { get; set; }
            public int OrderNumber { get; set; }
        public string CartNumber { get; set; }
        public string CartHolderName { get; set; }
        public DateTime ValidUntil { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public bool? IsSuccess { get; set; }
    }
}
