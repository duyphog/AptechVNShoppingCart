using System;
namespace Entities.Helpers
{
    public class SalesOrderParameters : QueryStringParameters
    {
        public Guid? UserId { get; set; }
    }
}
