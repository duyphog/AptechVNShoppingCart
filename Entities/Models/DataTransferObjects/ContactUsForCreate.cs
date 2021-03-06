using System;
namespace Entities.Models.DataTransferObjects
{
    public class ContactUsForCreate
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public int? Confirm { get; set; }
        public string Description { get; set; }
    }
}
