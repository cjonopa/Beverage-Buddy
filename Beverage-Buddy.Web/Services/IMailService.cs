using Beverage_Buddy.Web.Models;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}