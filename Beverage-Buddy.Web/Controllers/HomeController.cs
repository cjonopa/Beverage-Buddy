using Beverage_Buddy.Web.Models;
using Beverage_Buddy.Web.Services;
using Beverage_Buddy.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Beverage_Buddy.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService mailService;
        public HomeController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet("Contact")]
        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost("Contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // send email
                mailService.SendEmailAsync(
                    new MailRequest()
                    {
                        To = "abc@abc.com",
                        Subject = model.Subject,
                        Body = $"From: {model.Name} - {model.Email}, Message: {model.Message}"
                    });

                ViewBag.SentMessage = "Mail Sent";
            }

            return View();
        }
    }
}
