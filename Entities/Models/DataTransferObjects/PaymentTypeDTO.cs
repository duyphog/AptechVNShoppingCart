using System;
namespace Entities.Models.DataTransferObjects
{
    public class PaymentTypeDTO
    {
        public int Id { get; set; }
        public string PaymentTypeName { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
    }
}
