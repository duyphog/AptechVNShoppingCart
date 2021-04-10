using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class AppUserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual AppRole Role { get; set; }
        public virtual AppUser User { get; set; }
    }
}
