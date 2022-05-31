using BooksPlace.Data;
using BooksPlace.ExtensionMethods;
using BooksPlace.MessageBroker;
using BooksPlace.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Westwind.AspNetCore.LiveReload;

namespace BooksPlace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<BooksPlaceDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("BooksPlaceDbConnection"));
            });
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = true;

            }).AddEntityFrameworkStores<BooksPlaceDbContext>();
            services.ConfigureApplicationCookie(options => {

                options.Cookie.Name = "UserAuthCookie";
                options.LoginPath = "/Login/SignIn";
                options.LogoutPath = "";
                options.AccessDeniedPath = "/Login/AccessDenied";
            
            });
            services.AddRepository();
            services.AddControllers().AddJsonOptions(options => 
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddRabbitMq();
            services.AddLiveReload();
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseLiveReload();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
