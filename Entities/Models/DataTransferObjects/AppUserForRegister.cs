using System;
using System.Collections.Generic;

namespace Entities.Models.DataTransferObjects
{
    public class AppUserForRegister
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string Password { get; set; }

        public ICollection<Guid> RoleIds { get; set; }
    }
}
