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
            SalesOrders = new HashSet<SalesOrder>();
            TradeReturnRequests = new HashSet<TradeReturnRequest>();
            UserAddresses = new HashSet<UserAddress>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public long? Version { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<TradeReturnRequest> TradeReturnRequests { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
