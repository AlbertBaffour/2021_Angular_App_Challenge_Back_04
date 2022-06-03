using dcode_shop_back.Data;
using dcode_shop_back.Models;
using dcode_shop_back.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : Controller
    {
        IEmailSender _emailSender;
        ShopContext _context;
        
        public EmailSenderController(IEmailSender emailSender,ShopContext context)
        {
            _emailSender = emailSender;
            _context = context;
        }
        [HttpPost, Route("SendEmail")]
        public async void SendEmail(Mail mail, string Link)
        {
            try
            {
                //var msgOrder = await this.BuildOrderMessageAsync(mail.orderId);
                //mail.Message += msgOrder;
                mail.Message += "</table>" +
                    "</div></br>" +
                    "<p>Prettige feestdagen!</p>" +
                    "<p>Het Dcode-team</p>" +
                  "</div>";
                _emailSender.SendEmailAsync(mail.RecipientEmail, mail.RecipientName, mail.Subject, mail.Message, Link);
            }

            catch (Exception ex)
            {
                BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);
            }
        }
        //helpers
         private async Task<string> BuildOrderMessageAsync(int orderId)
        {
            var baseImgUrl = "https:////res.cloudinary.com//dk2ghbcex//image//upload//v1639839517//dsport//";
            var message = "<table style='width:100%'>";
            var orderProducts =await _context.OrderProducts.Where(o => o.OrderId == orderId).ToListAsync();
            foreach (var op in orderProducts)
            {
                var p = _context.Products.FindAsync(op.ProductId);
                message +=
                  "<tr>" +
                    "<td> <img class=' img-fluid' src='" + baseImgUrl + p.Result.Img + "' width='62' height='62'></td>" +
                    "<td> <p> <b>Aantal: </b> <b>" + op.Quantity + "</b> </p></td>" +
                    "<td> <p><b>Prijs: €</b> <b>" + op.CurrentPrice + "</b></p></td>" +
                  "</tr>";
            }
          
            return message;
        }
    }
}
