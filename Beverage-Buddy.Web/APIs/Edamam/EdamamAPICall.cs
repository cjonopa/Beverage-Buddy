using Beverage_Buddy.Web.APIs.Edamam.Models;
using Beverage_Buddy.Web.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.APIs.Edamam
{
    public class EdamamAPICall : IAPICall
    {
        private readonly APISettings apiSettings;

        public EdamamAPICall(IOptions<APISettings> apiSettings)
        {
            this.apiSettings = apiSettings.Value;
        }

        public async Task<ActionResult> RetrieveAllData()
        {
            string apiCall =
                $"{apiSettings.AccessPoint}?type={apiSettings.Type}&app_key={apiSettings.AppKey}" +
                $"&app_id={apiSettings.AppId}&dishType={apiSettings.DishType}";
            Result apiResult = new Result();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiCall))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    apiResult = JsonConvert.DeserializeObject<Result>(apiResponse);
                }
            }

            return null;
        }
    }
}
