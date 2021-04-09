using CA.ERP.WebApp.Blazor.Services;
using CA.ERP.WebApp.Blazor.ViewModels.PurchaseOrder;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<AuthorizationMessageHandler>();

            builder.Services.AddHttpClient(Constants.ApiErp, client => client.BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress:Erp").Value))
                 .AddHttpMessageHandler(cfg => {
                     var hadnler = cfg.GetRequiredService<AuthorizationMessageHandler>();
                     hadnler.ConfigureHandler(new List<string>() { builder.Configuration.GetSection("BaseAddress:Erp").Value, builder.Configuration.GetSection("BaseAddress:Erp").Value });
                     return hadnler;
                 });

            builder.Services.AddHttpClient(Constants.ApiIdentity, client => client.BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress:Identity").Value))
                .AddHttpMessageHandler(cfg => {
                    var hadnler = cfg.GetRequiredService<AuthorizationMessageHandler>();
                    hadnler.ConfigureHandler(new List<string>() { builder.Configuration.GetSection("BaseAddress:Erp").Value, builder.Configuration.GetSection("BaseAddress:Identity").Value });
                    return hadnler;
                });

            builder.Services.AddMudServices();

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Identity", options.ProviderOptions);
            });


            builder.Services.AddScoped<PurchaseOrderService>();


            builder.Services.AddScoped<PurchaseOrderIndexViewModel>();

            await builder.Build().RunAsync();
        }
    }
}
