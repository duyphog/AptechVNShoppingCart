using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class AppUser
    {
        public AppUser()
        {
            AppUserRoles = new HashSet<AppUserRole>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public long? CycleCount { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
