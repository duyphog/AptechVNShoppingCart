using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class UserAddress
    {
        public Guid Id { get; set; }
        public Guid? AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Contry { get; set; }
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
