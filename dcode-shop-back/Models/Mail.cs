using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Models
{
    public class Mail
    {
        public string RecipientEmail { get; set; }
        public string RecipientName{ get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int orderId { get; set; }
    }
}
