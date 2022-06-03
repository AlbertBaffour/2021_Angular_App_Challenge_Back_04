using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dcode_shop_back.Models
{
    public class Order
    {
        public enum status
        {
            basket,
            ordered,
            processing,
            shipped,
            delivered
        }

        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public status Status { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
      
    /*    public decimal TotalPrice { 
            get {
                Decimal totalPrice = 0;
                foreach(OrderProduct op in OrderProducts)
                {
                    totalPrice = totalPrice + op.Subtotal;
                }
                return totalPrice;
            }
        }*/
    }

}
