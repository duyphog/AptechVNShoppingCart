using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("product")]
    public class Product
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
