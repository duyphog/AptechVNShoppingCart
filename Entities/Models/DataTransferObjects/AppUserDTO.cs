using System;
using System.Collections.Generic;

namespace Entities.Models.DataTransferObjects
{
    public class AppUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public long? Version { get; set; }
        public IEnumerable<AppRoleDTO> Roles { get; set; }
    }
}
