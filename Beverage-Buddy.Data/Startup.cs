using Beverage_Buddy.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Beverage_Buddy.Data.APIs.CocktailDb;
using Beverage_Buddy.Data.APIs.CocktailDb.Models;
using Beverage_Buddy.Data.APIs.CocktailDb.Settings;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Beverage_Buddy.Data.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Beverage_Buddy.Data
{
    public class Startup
    {

        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<RecipeUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<BeverageBuddyDbContext>();
                        
            services.AddDbContext<BeverageBuddyDbContext>();
            
            services.AddTransient<BeverageBuddySeeder>();
            services.AddTransient<IConverterService<Drink, DrinkResult>, DrinkConverterService>();
            services.AddScoped<CocktailDbApiCaller, CocktailDbApiCaller>();
            services.Configure<ApiSettings>(configuration.GetSection("APISettings"));

            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 1);
                opt.ReportApiVersions = true;
            });

            services.AddMvc(opt => opt
                    .EnableEndpointRouting = false)
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );


            services.AddScoped<IRepository<Recipe, int>, RecipeRepository>();
            services.AddScoped<IRepository<Drink, string>, DrinkRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
