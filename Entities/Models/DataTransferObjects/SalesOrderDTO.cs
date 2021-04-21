using System;
namespace Entities.Models.DataTransferObjects
{
    public class SalesOrderDTO
    {
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

        public decimal TotalAmount { get; set; }
    }
}
