using Beverage_Buddy.Web.APIs.Edamam.Models;
using Beverage_Buddy.Web.Edamam.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.APIs.Edamam
{
    public class EdamamAPICaller
    {
        private readonly APISettings apiSettings;

        private string BaseUrl { get; set; }

        public EdamamAPICaller(IOptions<APISettings> apiSettings)
        {
            this.apiSettings = apiSettings.Value;
            BaseUrl = this.apiSettings.AccessPoint;
    }

        public async Task<Result> RetrieveDrinkRecipes(string? cont = null)
        {
            string query = $"?type={apiSettings.Type}&app_key={apiSettings.AppKey}" +
                    $"&app_id={apiSettings.AppId}&dishType={apiSettings.DishType}";

            if (!string.IsNullOrEmpty(cont)) query += $"&_cont={cont}";

            Result apiResult = new Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.GetAsync(query))
                {
                    var apiResponse = response.Content.ReadAsStringAsync().Result;
                    apiResult = JsonConvert.DeserializeObject<Result>(apiResponse);
                }
            }

            return apiResult;
        }
    }
}
