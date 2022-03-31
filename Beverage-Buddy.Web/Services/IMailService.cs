namespace Beverage_Buddy.Web.Services
{
    public interface IMailService
    {
        void SendMessage(string to, string subject, string body);
    }
}