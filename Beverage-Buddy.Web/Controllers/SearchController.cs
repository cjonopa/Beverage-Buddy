using Beverage_Buddy.Web.APIs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAPICall apiCall;

        public SearchController(IAPICall apiCall)
        {
            this.apiCall = apiCall;
        }

        public IActionResult Index()
        {
            apiCall.RetrieveAllData();
            return View();
        }
    }
}
