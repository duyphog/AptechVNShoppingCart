using System;
namespace Entities.Helpers
{
    public class ProductParameters : QueryStringParameters
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int? PrefixStock { get; set; }
        public int? StockValue { get; set; }
    }
}
