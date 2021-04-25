using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DataTransferObjects
{
    public class SalesOrderMasterForCreate
    {
        public int OrderStatusId { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }
        [Required]
        public string DeliveryTypeId { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public string Contry { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string OrderNote { get; set; }
    }
}
