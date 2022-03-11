using BeverageBuddy.Data.Services;
using System.Web.Mvc;

namespace BeverageBuddy.Web.Controllers
{
    public class HomeController : Controller
    {
        IRecipeData db;

        public HomeController(IRecipeData db)
        {
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}