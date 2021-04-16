using CA.ERP.DataAccess;
using CA.ERP.Reporting.Mapping;
using CA.ERP.Reporting.Repositories;
using CA.ERP.Reporting.Services;
using jsreport.AspNetCore;
using jsreport.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Reporting
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
            services.AddDbContext<CADataContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddCors(option => {
                option.AddDefaultPolicy(builder => {
                    builder
                    .SetIsOriginAllowed(origin => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

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

            

          
            string reportingServer = Configuration.GetSection("ReportServer")?.Value ?? "http://localhost:5488";
            services.AddJsReport(new ReportingService(reportingServer));

            services.AddAutoMapper(typeof(PurchaseOrderMapping).Assembly);

            services.AddScoped<IBarcodeService, BarcodeService>();


            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();

            services.AddRazorPages();
            services.AddControllersWithViews();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //temp disable
            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseCors();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
