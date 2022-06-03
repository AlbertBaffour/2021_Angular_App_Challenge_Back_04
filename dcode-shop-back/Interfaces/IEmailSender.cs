using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Services
{
    public interface IEmailSender
    {
        void SendEmailAsync(string recipientEmail, string recipientFirstName, string subject, string message, string Link);
    }
}
