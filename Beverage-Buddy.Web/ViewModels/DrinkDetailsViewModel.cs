using Beverage_Buddy.Data.Models;
using Newtonsoft.Json;

namespace Beverage_Buddy.Web.ViewModels
{
    public class DrinkDetailsViewModel
    {
        public Drink Drink { get; set; }
        public bool IsAuthenticated { get; set; }

        public void ConvertJsonResponse(string webResponse)
        {
            Drink = JsonConvert.DeserializeObject<Drink>(webResponse);
        }
    }
}
