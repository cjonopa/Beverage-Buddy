using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.APIs.Edamam.Models
{
    public class Link
    {
        public Next Next { get; set; }
        public Self Self { get; set; }
    }

    public class Next
    {
        public string Href { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            string next = "";
            int startAt = 0;
            int endAt = 0;
            int length = 0;

            startAt = Href.IndexOf("cont=") + 5;
            endAt = Href.IndexOf('&', startAt);
            length = endAt - startAt;

            next = Href.Substring(startAt, length);

            return next;
        }
    }

    public class Self
    {
        public string Href { get; set; }
        public string Title { get; set; }
    }
}
