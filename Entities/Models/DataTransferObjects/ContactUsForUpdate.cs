using System;
namespace Entities.Models.DataTransferObjects
{
    public class ContactUsForUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
    }
}
