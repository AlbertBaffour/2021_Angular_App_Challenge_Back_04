using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dcode_shop_back.Models
{
    public class Product
    {
    
        public int id { get; set; }
        public string Brand { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)]")]
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } 
        public int QuantityInStock  { get; set; } 
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Img { get; set; }

    }
}
