using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dcode_shop_back.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18,2)]")]
        public Decimal CurrentPrice { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set;  }
        public Decimal Subtotal {
            get {
                return CurrentPrice*Quantity;
            }
        }

  

    }
}
