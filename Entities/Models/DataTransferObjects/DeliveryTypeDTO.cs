using System;
namespace Entities.Models.DataTransferObjects
{
    public class DeliveryTypeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
    }
}
