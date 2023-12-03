using FluentValidation;
using FluentValidation.AspNetCore;
using Humanizer.Inflections;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.ConstrainedExecution;
using UdemyMicroservices.Shared.Services;
using UdemyMicroservices.Web.Extensions;
using UdemyMicroservices.Web.Handler;
using UdemyMicroservices.Web.Helpers;
using UdemyMicroservices.Web.Models;
using UdemyMicroservices.Web.Services;
using UdemyMicroservices.Web.Services.Interfaces;
using UdemyMicroservices.Web.Validators;

namespace UdemyMicroservices.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);            
           
            builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
            builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));

            

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAccessTokenManagement();
            builder.Services.AddSingleton<PhotoHelper>();
            builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            builder.Services.AddScoped<ClientCredentialTokenHandler>();

            builder.Services.AddHttpClientServices(builder.Configuration);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {
                opt.LoginPath = "/Auth/SignIn";
                opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                opt.SlidingExpiration = true;
                opt.Cookie.Name = "udemywebcookie";
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddFluentValidationAutoValidation(fv => 
                fv.DisableDataAnnotationsValidation = true)
                .AddValidatorsFromAssemblyContaining<CourseCreateInputValidator>();
           
                   

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}