using System;
namespace Entities.Models.DataTransferObjects
{
    public class SalesOrderDetailDTO
    {
        public Guid Id { get; set; }
        public int Record { get; set; }
        public Guid? SalesOrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
