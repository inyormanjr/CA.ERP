using CA.ERP.WebApp.Blazor.Options;
using CA.ERP.WebApp.Blazor.Services;
using CA.ERP.WebApp.Blazor.ViewModels.Management.User;
using CA.ERP.WebApp.Blazor.ViewModels.PurchaseOrder;
using CA.ERP.WebApp.Blazor.ViewModels.Stock;
using CA.ERP.WebApp.Blazor.ViewModels.StockReceive;
using CA.ERP.WebApp.Blazor.ViewModels.StockTransfer;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using Polly;
using Serilog;
using Syncfusion.Blazor;
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
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();


            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Logging.AddSerilog();

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<AuthorizationMessageHandler>();



            builder.Services.AddHttpClient(Constants.ApiErp, client => client.BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress:Erp").Value))
                 .AddHttpMessageHandler(cfg =>
                 {
                     var hadnler = cfg.GetRequiredService<AuthorizationMessageHandler>();
                     hadnler.ConfigureHandler(new List<string>() { builder.Configuration.GetSection("BaseAddress:Erp").Value, builder.Configuration.GetSection("BaseAddress:Erp").Value });
                     return hadnler;
                 }).AddTransientHttpErrorPolicy(BuildHttpErrorPolicy);

            builder.Services.AddHttpClient(Constants.ApiIdentity, client => client.BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress:Identity").Value))
                .AddHttpMessageHandler(cfg =>
                {
                    var hadnler = cfg.GetRequiredService<AuthorizationMessageHandler>();
                    hadnler.ConfigureHandler(new List<string>() { builder.Configuration.GetSection("BaseAddress:Erp").Value, builder.Configuration.GetSection("BaseAddress:Identity").Value });
                    return hadnler;
                }).AddTransientHttpErrorPolicy(BuildHttpErrorPolicy);

            builder.Services.AddMudServices(config => {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;


                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;

            });

            builder.Services.AddSyncfusionBlazor();


            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Identity", options.ProviderOptions);
            });



            builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<BranchService>();
            builder.Services.AddScoped<IMasterProductService, MasterProductService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBranchService, BranchService>();
            builder.Services.AddScoped<IStockReceiveService, StockReceiveService>();
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddScoped<IBrandService, BrandService>();

            builder.Services.AddScoped<PurchaseOrderListViewModel>();
            builder.Services.AddScoped<PurchaseOrderCreateViewModel>();

            builder.Services.AddScoped<UserListViewModel>();
            builder.Services.AddScoped<UserCreateViewModel>();
            builder.Services.AddScoped<UserDetailViewModel>();
            builder.Services.AddScoped<UserEditViewModel>();

            builder.Services.AddScoped<GenerateFromPurchaseOrderDialogViewModel>();
            builder.Services.AddScoped<StockReceiveListViewModel>();
            builder.Services.AddScoped<StockReceiveEditViewModel>();
            builder.Services.AddScoped<StockReceiveDetailViewModel>();

            builder.Services.AddScoped<ChangePasswordViewModel>();

            builder.Services.AddScoped<StockListViewModel>();

            builder.Services.AddScoped<StockTransferCreateViewModel>();

            builder.Services.Configure<BaseAddresses>(baseAddresses => builder.Configuration.GetSection("BaseAddress").Bind(baseAddresses));

            await builder.Build().RunAsync();
                
        }

        private static IAsyncPolicy<HttpResponseMessage> BuildHttpErrorPolicy(PolicyBuilder<HttpResponseMessage> builder)
        {
            return builder.WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    });
        }


        public static Policy GetRetryPolicy()
        {
            return Policy.Handle<HttpRequestException>().WaitAndRetry(3, count => TimeSpan.FromSeconds(count * 10));
        }
    }
}
