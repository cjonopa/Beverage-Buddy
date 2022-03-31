using Beverage_Buddy.Data.Services;
using Beverage_Buddy.Web.Services;
using Beverage_Buddy.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService mailService;
        private readonly IRecipeRepository db;

        public HomeController(IMailService mailService, IRecipeRepository db)
        {
            this.mailService = mailService;
            this.db = db;
        }

        public ActionResult Index()
        {
            var model = db.GetAll();

            return View(model);
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
                mailService.SendMessage("abc@abc.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.SentMessage = "Mail Sent";
            }

            return View();
        }
    }
}
