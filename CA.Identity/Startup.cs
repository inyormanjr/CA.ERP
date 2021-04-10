using CA.Identity.Data;
using CA.Identity.Models;
using IdentityServer4.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using IdentityServer4.Services;
using CA.Identity.Services;
using CA.Identity.Repository;
using IdentityServer4;
using System;

namespace CA.Identity
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();



            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                      .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();



            services.AddIdentityServer()
              .AddDeveloperSigningCredential()
                      .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                      {
                          options.Clients.Add(new Client
                          {
                              ClientId = "Erp",
                              AllowedGrantTypes = GrantTypes.Code,
                              RequirePkce = true,
                              RequireClientSecret = false,
                              AllowedCorsOrigins = { "https://localhost:6001" },
                              AllowedScopes =
                                {
                                    IdentityServerConstants.StandardScopes.OpenId,
                                    IdentityServerConstants.StandardScopes.Profile,
                                    "Erp"
                                },
                              RedirectUris = { "https://localhost:6001/authentication/login-callback" },
                              PostLogoutRedirectUris = { "https://localhost:6001/authentication/logout-callback" }
                          });
                          options.ApiScopes.Add(new ApiScope("Erp"));
                          options.ApiResources.AddApiResource("Erp", cfg => cfg.WithScopes("Erp").AllowAllClients());
                      });

            services.AddAuthentication()
                .AddIdentityServerJwt();


            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserBranchRepository, UserBranchRepository>();

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

            services.AddControllersWithViews();
            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddMvc();

            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();


            app.UseCors();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();


            //wee
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });

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
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    if (context.Database.IsRelational())
                    {
                        context.Database.Migrate();
                    }


                }
            }
        }
    }
}
