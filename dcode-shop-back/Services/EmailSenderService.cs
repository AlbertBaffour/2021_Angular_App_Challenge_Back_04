using dcode_shop_back.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net;

namespace dcode_shop_back.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly EmailSender _emailSender;
        public EmailSenderService(IOptions<EmailSender> emailSender)
        {
            _emailSender = emailSender.Value;
        }



        public async void SendEmailAsync(string recipientEmail, string recipientName, string subject, string message, string Link)
        {

            var messageS = new MimeMessage();
            messageS.From.Add(new MailboxAddress(_emailSender.SenderName, _emailSender.SenderEmail));
            messageS.To.Add(new MailboxAddress(recipientName, recipientEmail));
            messageS.Subject = subject != "" ? subject : "Test @ Dcode";

            messageS.Body = new TextPart("html")
            {
                Text = message != "" ? message : "Bedankt! Test @ dcode!"
            };


            var client = new SmtpClient();
            await client.ConnectAsync(_emailSender.Server, _emailSender.Port, true);
            await client.AuthenticateAsync(new NetworkCredential(_emailSender.SenderEmail, _emailSender.Password));
            await client.SendAsync(messageS);
            await client.DisconnectAsync(true);

        }
    }
}