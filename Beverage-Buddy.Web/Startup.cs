using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Beverage_Buddy.Data.Services;
using Beverage_Buddy.Web.Settings;
using Beverage_Buddy.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Beverage_Buddy.Web
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
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<RecipeUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<BeverageBuddyDbContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            if (env.IsDevelopment())
            {
                services.AddTransient<IMailService, NullMailService>();
            } 
            else if (env.IsProduction())
            {
                services.AddTransient<IMailService, MimeMailService>();
                services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            }

            services.AddDbContext<BeverageBuddyDbContext>();
            
            services.AddScoped<IRepository<Recipe, int>, RecipeRepository>();
            services.AddScoped<IRepository<Drink, string>, DrinkRepository>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(cfg =>
            {
                cfg.MapRazorPages();

                cfg.MapControllerRoute("Default",
                    "/{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index" });
            });
        }
    }
}
