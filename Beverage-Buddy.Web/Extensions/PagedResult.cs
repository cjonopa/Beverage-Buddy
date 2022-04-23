using System.Collections.Generic;
using System.Linq;
using Beverage_Buddy.Web.Services;

namespace Beverage_Buddy.Web.Extensions
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
