using Beverage_Buddy.Web.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Beverage_Buddy.Web.Services
{
    public class NullMailService : Controller, IMailService
    {
        private readonly ILogger<NullMailService> logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            this.logger = logger;
        }

        public Task SendEmailAsync(MailRequest mailRequest)
        {
            logger.LogInformation($"To: {mailRequest.To} Subject: {mailRequest.Subject} Body: {mailRequest.Body}");
            return Task.CompletedTask;
        }

        public void SendMessage(string to, string subject, string body)
        {
            logger.LogInformation($"To: {to} Subject: {subject} Body: {body}");
        }
    }
}
