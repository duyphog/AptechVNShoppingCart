using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ContactUs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public int? Confirm { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ConfirmDescription { get; set; }
    }
}
