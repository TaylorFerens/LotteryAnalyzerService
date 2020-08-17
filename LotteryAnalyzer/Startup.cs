using LotteryAnalyzer.Classes;
using LotteryAnalyzer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LotteryAnalyzer
{
    public class Startup
    {
        #region Public Variables

        public IConfiguration Configuration { get; }
        public IHostEnvironment CurrentEnvironment { get; set; }

        #endregion
        #region Constructors

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        #endregion
        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                // Configure AppSettings Dependency Injection
                ConfigureAppSettings(services);

                // Configure EF Core DB Connection
                ConfigureDatabaseConnection(services);

                // Configure Depedency Injection for Application Services
                ConfigureAppServices(services);

                // Configure MVC
                ConfigureMVC(services);

                // Configure CORs
                ConfigureCORs(services);

                // Configure Cookie Authentication
                ConfigureCookieAuthentication(services);

                // Configure Cookie Policies
                ConfigureCookiePolicies(services);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors();
            // app.UseHttpsRedirection();

            app.UseMvc();
        }

        #endregion
        #region Private Methods

        private void ConfigureAppSettings(IServiceCollection services)
        {
            IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
        }

        private void ConfigureDatabaseConnection(IServiceCollection services)
        {
            // To Do: Add a switch depending on build environment to determine database to connect to (i.e. dev, test, staging, prod, etc)

            // Dependency Injection for Reminder Database Context
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DatabaseContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("Dev"))
            );
        }

        private void ConfigureAppServices(IServiceCollection services)
        {
            // Configure Dependency Injection for application services (i.e the Service Classes)
            services.AddScoped<LotteryAnalyzerService>();
            services.AddScoped<SearchStatisticsService>();
            services.AddScoped<LotteryService>();

            // Background Threads
            services.AddHostedService<DailyAnalysisService>();

            // Various other services
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        }

        private void ConfigureMVC(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                // Here we use the policy created above to specify the type of Authorization Filter we want to use
                // This, in essence, locks down every public web method, except where [AllowAnonymous] is defined
                //options.Filters.Add(new AuthorizeFilter());

                options.EnableEndpointRouting = false;

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddNewtonsoftJson(
                options =>
                {
                    // Here we remove the reference loop handling so when we serialize "many-to-many" entities, we will not get a run-time error
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }
            );
        }

        private void ConfigureCORs(IServiceCollection services)
        {
            string[] origins = {
                "http://localhost:4200",
                "http://192.168.0.18:4200"
            };

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowCredentials();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.WithOrigins(origins);
                });
            });
        }

        private void ConfigureCookieAuthentication(IServiceCollection services)
        {
            // configure cookie authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                // This always needs to be true, so that the front end is never able to read cookie data
                options.LoginPath = "/login";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.Name = "LotteryAnalyzer";

                options.Events.OnRedirectToLogin = context =>
                {
                    // Return a 401 error if unauthorized and redirected to the Login
                    context.Response.StatusCode = 401;

                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    // Return a 401 error if unauthorized
                    context.Response.StatusCode = 401;

                    return Task.CompletedTask;
                };
            });
        }

        private void ConfigureCookiePolicies(IServiceCollection services)
        {
            // Policy that will be applied to a general filter of this web service
            // This policy ensures that, by default, all exposed methods require authentication 
            // EXCEPT when the [AllowAnonymous] tag is explicitly coded for a web method
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.Always;
            });
        }

        #endregion
    }
}
