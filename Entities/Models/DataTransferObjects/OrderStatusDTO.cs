using System;
namespace Entities.Models.DataTransferObjects
{
    public class OrderStatusDTO
    {
        public int Id { get; set; }
        public string OrderStatusName { get; set; }
        public string Description { get; set; }
    }
}
