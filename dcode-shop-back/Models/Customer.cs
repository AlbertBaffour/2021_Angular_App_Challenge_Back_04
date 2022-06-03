using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAndNumber { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public ICollection<Order> Orders { get; set; }
        public ICollection<Favourite> Favourites { get; set; }
    }
}
