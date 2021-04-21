using System;
namespace Entities.Models.DataTransferObjects
{
    public class SalesOrderDetailForCreate
    {
        public int Record { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
