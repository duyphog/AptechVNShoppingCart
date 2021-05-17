using System;
namespace Entities.Models.DataTransferObjects
{
    public class SaleOrderForTradeOrReturn
    {
        public string SalesOrderId { get; set; }
        public bool IsTrade { get; set; }
        public string Description { get; set; }
        public int  Quantity { get; set; }
    }
}
