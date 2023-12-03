using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using UdemyMicroservices.Gateway.DelegateHandlers;

namespace UdemyMicroservices.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", true,true)
                                 .AddJsonFile("configuration.development.json", true,true)
                                 .AddEnvironmentVariables();

            builder.Services.AddHttpClient<TokenExchangeDelegateHandler>();

            builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", opt =>
            {
                opt.Authority = builder.Configuration.GetSection("IdentityServerURL").Value;
                opt.Audience = "resource_gateway";
                opt.RequireHttpsMetadata = false;
            });

            builder.Services.AddOcelot(builder.Configuration).AddDelegatingHandler<TokenExchangeDelegateHandler>();

            var app = builder.Build();

            app.UseOcelot().GetAwaiter().GetResult();

            app.Run();
                       
        }
    }
}