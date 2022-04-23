using Beverage_Buddy.Web.Models;
using Beverage_Buddy.Web.Services;
using Beverage_Buddy.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;

namespace Beverage_Buddy.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly UserManager<RecipeUser> userManager;
        private readonly SignInManager<RecipeUser> signInManager;
        private readonly IMailService mail;

        public AccountController(ILogger<AccountController> logger,
            UserManager<RecipeUser> userManager,
            SignInManager<RecipeUser> signInManager,
            IMailService mail)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mail = mail;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new RecipeUser()
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.PasswordSignInAsync(
                        model.Email,
                        model.Password,
                        false, false);

                    await mail.SendEmailAsync(
                        new MailRequest()
                        {
                            To = model.Email,
                            Subject = "Registration",
                            Body = "You have successfully registered with FridgeMeal."
                        });

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Failed to Login.");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
