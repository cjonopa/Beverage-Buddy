using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Data.Infrastructure
{
    public static class Extension
    {
        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return true;
            }

            return collection.All(item => item == null);
        }
    }
}
