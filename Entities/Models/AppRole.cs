using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class AppRole
    {
        public AppRole()
        {
            AppUserRoles = new HashSet<AppUserRole>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
