using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.APIs
{
    public interface IAPICall
    {
        Task<ActionResult> RetrieveAllData();
    }
}
