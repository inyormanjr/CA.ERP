using AutoMapper;
using CA.ERP.DataAccess;
using CA.ERP.DataAccess.AutoMapperProfiles;
using CA.ERP.DataAccess.Repositories;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.UserAgg;
using DtoMapping = CA.ERP.WebApp.Mapping;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using CA.ERP.WebApp.Helpers;
using CA.ERP.Domain.SupplierAgg;
using FluentValidation;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

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
            services.AddHealthChecks();


            services.AddDbContext<CADataContext>(dbc =>

                dbc.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"), x=> x.MigrationsAssembly("CA.ERP.DataAccess")));

            services.AddSwaggerGen(setup => {
                //add xml for endpoint description.
                var docs = Path.Combine(System.AppContext.BaseDirectory, "CA.ERP.WebApp.xml");
                if (File.Exists(docs))
                {
                    setup.IncludeXmlComments(docs);
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

            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors();

            services.AddAutoMapper(typeof(DtoMapping.BranchMapping).Assembly, typeof(UserMapping).Assembly);

            services.AddHttpContextAccessor();

            //register web api helpers
            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(UserHelper))
                .AddClasses(classes => classes.AssignableTo<IHelper>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            //register repositories
            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(UserRepository))
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            //register factories
            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(UserFactory))
                .AddClasses(classes => classes.AssignableTo<IFactory>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            //register services
            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(UserService))
                .AddClasses(classes => classes.AssignableTo<ServiceBase>())
                .AsSelf()
                .WithScopedLifetime()
            );

            //register helpers
            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(PasswordManagementHelper))
                .AddClasses(classes => classes.AssignableTo<HelperBase>())
                .AsSelf()
                .WithScopedLifetime()
                );

            //register validators
            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(SupplierValidator))
                .AddClasses(classes => classes.AssignableTo<IValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                );

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer( options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    
                };

                options.Events = new JwtBearerEvents { 
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        context.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme);
            // In production, the Angular files will be served from this directory
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});

            //override asp.net validation to nothing    

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            //temp disable
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Citi App API V1");
            });


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
    }
}
