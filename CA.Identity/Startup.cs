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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Polly;
using System.Linq;
using Microsoft.AspNetCore.Http;

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



            services.AddDefaultIdentity<ApplicationUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                      .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();



            services.AddIdentityServer()
              .AddDeveloperSigningCredential()
                      .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                      {
                          options.Clients.Add(new Client
                          {
                              ClientId = "erp",
                              AllowedGrantTypes = GrantTypes.Code,
                              RequirePkce = true,
                              RequireClientSecret = false,
                              AllowedCorsOrigins = Configuration.GetSection("ErpClient:AllowedCorsOrigins").Get<string[]>(),
                              AllowedScopes =
                                {
                                    IdentityServerConstants.StandardScopes.OpenId,
                                    IdentityServerConstants.StandardScopes.Profile,
                                    IdentityServerConstants.LocalApi.ScopeName,
                                    "erp", "report"
                                },
                              RedirectUris = Configuration.GetSection("ErpClient:RedirectUris").Get<string[]>(),
                              PostLogoutRedirectUris = Configuration.GetSection("ErpClient:PostLogoutRedirectUris").Get<string[]>(),
                          });

                          options.ApiScopes.Add(new ApiScope("erp"));
                          options.ApiScopes.Add(new ApiScope("report"));
                          options.ApiScopes.Add(new ApiScope(IdentityServerConstants.LocalApi.ScopeName));
                          options.ApiResources.AddApiResource("erp", cfg => cfg.WithScopes("erp").AllowAllClients());
                          options.ApiResources.AddApiResource("report", cfg => cfg.WithScopes("report").AllowAllClients());
                          options.ApiResources.AddApiResource(IdentityServerConstants.LocalApi.ScopeName, cfg => cfg.WithScopes(IdentityServerConstants.LocalApi.ScopeName).AllowAllClients());
                      });

            services.AddAuthentication()
                .AddIdentityServerJwt();


            services.AddLocalApiAuthentication();


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

            services.AddMvc();

            services.AddSwaggerGen();


            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.None });
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

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

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
                    using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
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
