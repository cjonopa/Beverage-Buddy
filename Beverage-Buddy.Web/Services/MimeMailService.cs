﻿using Beverage_Buddy.Web.Models;
using Beverage_Buddy.Web.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.Services
{
    /// <summary>
    /// class created using https://www.c-sharpcorner.com/article/send-email-using-asp-net-core-5-web-api/
    /// </summary>
    public class MimeMailService : IMailService
    {
        private readonly MailSettings mailSettings;

        public MimeMailService(IOptions<MailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.To));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    using(var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            var smtp = new SmtpClient();
            
            smtp.Connect(mailSettings.Host, mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSettings.Mail, mailSettings.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
 
        }

        public void SendMessage(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
