using AutoMapper;
using CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.CreatePurchaseOrder;
using CA.ERP.Application.DomainEventHandlers.Supplier;
using CA.ERP.DataAccess;
using CA.ERP.DataAccess.Repositories;
using CA.ERP.Domain.Common.Rounding;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.EventBus;
using CA.ERP.Domain.Core.Repository;
using CA.ERP.Domain.IdentityAgg;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.Services;
using CA.ERP.Infrastructure.EventBus;
using CA.ERP.WebApp.ActionFilters;
using CA.ERP.WebApp.HealthChecks;
using CA.ERP.WebApp.Infrastructure;
using jsreport.AspNetCore;
using jsreport.Client;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using DtoMapping = CA.ERP.WebApp.Mapping;

namespace CA.ERP.WebApp
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck<DatabaseCheck>("Database check", HealthStatus.Unhealthy);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardLimit = 2;  //Limit number of proxy hops trusted
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();

                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });




            services.AddDbContext<CADataContext>(dbc =>

                dbc.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("CA.ERP.DataAccess")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen(setup =>
            {
                //add xml for endpoint description.
                var docs = Path.Combine(System.AppContext.BaseDirectory, "CA.ERP.WebApp.xml");
                if (File.Exists(docs))
                {
                    setup.IncludeXmlComments(docs);
                }
                //add domain xml
                var domaindocs = Path.Combine(System.AppContext.BaseDirectory, "CA.ERP.Domain.xml");
                if (File.Exists(domaindocs))
                {
                    setup.IncludeXmlComments(domaindocs);
                }

                //add security scheme
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                setup.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                        {securityScheme, new string[] { }}

                });

            });

            services.AddControllersWithViews(
                option =>
                {
                    option.Filters.Add<RequestProcessingTimeFilter>();
                    option.Filters.Add<RequestTimestampSetter>();
                }
                )
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder
                    .SetIsOriginAllowed(origin => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            string reportingServer = Configuration.GetSection("ReportServer")?.Value ?? "http://jsreportserver:5488";
            services.AddJsReport(new ReportingService(reportingServer));


            services.AddAutoMapper(typeof(DtoMapping.BranchMapping).Assembly, typeof(DataAccess.AutoMapperProfiles.BranchMapping).Assembly);

            services.AddHttpContextAccessor();


            //register repositories
            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(MasterProductRepository))
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );


            //manual
            services.AddScoped<IRoundingCalculator, NearestFiveCentRoundingCalculator>();

            services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {

                options.Authority = Configuration.GetSection("Identity:Authority").Value;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };



                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if (accessToken.ToString() != null)
                        {
                            var path = context.HttpContext.Request.Path;
                            context.Token = accessToken;

                        }
                        return Task.CompletedTask;
                    }
                };
            });


            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            //override asp.net validation to nothing    



            //add principal/user tranformer
            //services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

            //add mediator
            services.AddMediatR(typeof(CreatePurchaseOrderCommand));

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) => cfg.Host(Configuration.GetSection("RabbitMQ:Host").Value, "/", h =>
                {

                }));
                x.AddConsumersFromNamespaceContaining<PurchaseOrderCreatedHandler>();
            });

            services.AddMassTransitHostedService();

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IIdentityProvider, IdentityProvider>();
            services.AddScoped<IPurchaseOrderBarcodeGenerator, PurchaseOrderBarcodeGenerator>();
            services.AddScoped<IEventBus, MassTransitEventBus>();
            services.AddScoped<IStockReceiveGeneratorFromPurchaseOrderService, StockReceiveGeneratorFromPurchaseOrderService>();
            services.AddScoped<IStockReceiveGeneratorFromStockTransferService, StockReceiveGeneratorFromStockTransferService>();
            services.AddScoped<IStockNumberService, StockNumberService>();
            services.AddScoped<ICommitStockReceiveFromPurchaseOrderService, CommitStockReceiveFromPurchaseOrderService>();
            services.AddScoped<IStockTransferNumberGenerator, StockTransferNumberGenerator>();
            services.AddScoped<INumberToCodedStringConverter, NumberToCodedStringConverter>();



            //set culture info
            var cultureInfo = new CultureInfo("en-PH");
            cultureInfo.NumberFormat.CurrencySymbol = "₱";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;

            //temp disable
            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();



            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Citi App API V1");
            });


            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();


            //app.UseMiddleware<ErrorLoggingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapHealthChecks("/health");
            });




            bool.TryParse(Environment.GetEnvironmentVariable("DISABLE_SPA"), out bool disbaleSpa);
            if (!disbaleSpa)
            {
                if (!env.IsDevelopment())
                {
                    app.UseSpaStaticFiles();
                }

                app.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            }


        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            var policy = Policy
               .Handle<Exception>()
               .WaitAndRetry(new[]
               {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
               });

            policy.Execute(() =>
            {


                using (var serviceScope = app.ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<CADataContext>())
                    {
                        if (context.Database.IsRelational())
                        {
                            context.Database.Migrate();
                        }

                    }
                }
            });
        }


    }
}
