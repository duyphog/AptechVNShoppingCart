using System;
namespace Entities.Models.DataTransferObjects
{
    public class SalesOrderForCreate
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Contry { get; set; }
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string OrderNote { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
