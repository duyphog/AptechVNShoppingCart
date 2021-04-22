using System;
namespace Entities.Helpers
{
        
    public class AppUserParameters : QueryStringParameters
    {
        public Guid? RoleID { get; set; }
    }
}
