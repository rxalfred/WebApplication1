using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using WebApplication2.Middleware.Culture;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Model.CoreTestModel;
using ContosoUniversity.BusinessLogic.Logic.TestLogic;
using ContosoUniversity.Model.BusinessObject;
using ContosoUniversity.Bootstraper.AutofacModule;
using ContosoUniversity.DataAccessLayer.Repository;
using ContosoUniversity.DataAccessLayer.Repository.Base;
using ContosoUniversity.BusinessLogic.Logic;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using FluentValidation.AspNetCore;
using FluentValidation;
using WebApplication2.ViewModel;
using WebApplication2.Infrastructure.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication2.Providers;
using Microsoft.AspNetCore.Authorization;
using ContosoUniversity.Bootstraper.Validators;

namespace WebApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            env.ConfigureNLog("nlog.config");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add the localization services to the services container
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Duration = 60
                    });
                options.CacheProfiles.Add("VaryByHeader",
                    new CacheProfile()
                    {
                        Duration = 60,
                        VaryByHeader = "User-Agent",
                    });
                options.CacheProfiles.Add("Never",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            })
            .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<IBaseValidator>())
            // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            // Add support for localizing strings in data annotations (e.g. validation messages) via the
            // IStringLocalizer abstractions.
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory)
                => factory.Create(typeof(Welcome));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://www.example.com"));
            });

            services.AddResponseCompression();

            //services.Configure<RequestLocalizationOptions>(
            //opts =>
            //{
            //    var supportedCultures = new List<CultureInfo>
            //    {
            //        new CultureInfo("en"),
            //        new CultureInfo("zh"),
            //        new CultureInfo("zh-CN"),
            //    };
            //    //opts.DefaultRequestCulture = new RequestCulture("en");
            //    opts.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
            //    // Formatting numbers, dates, etc.
            //    opts.SupportedCultures = supportedCultures;
            //    // UI strings that we have localized.
            //    opts.SupportedUICultures = supportedCultures;
            //});

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("fr"),
                    new CultureInfo("zh"),
                    new CultureInfo("ar-YE")
                };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;

                // You can change which providers are configured to determine the culture for requests, or even add a custom
                // provider with your own logic. The providers will be asked in order to provide a culture for each request,
                // and the first to provide a non-null result that is in the configured supported cultures list will be used.
                // By default, the following built-in providers are configured:
                // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
                // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
                // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
                //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                //{
                //  // My custom request culture logic
                //  return new ProviderCultureResult("en");
                //}));
            });

            services.Configure<AppSettings>(Configuration.GetSection("Configuration"));
            //services.AddScoped<AppSettings>();

            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));

            // Logics
            services.AddTransient(typeof(IStudentLogic), typeof(StudentLogic));

            // Validators
            services.AddTransient<IValidator<RegistrationViewModel>, RegistrationViewModelValidator>();

            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("Hintalk"));

            // Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                //options.Cookie.Name = CookieAuthenticationDefaults.CookiePrefix;
                //options.Cookie.Domain = "contoso.com";
                options.Cookie.Path = "/";
                //options.Cookie.HttpOnly = true;
                //options.Cookie.SameSite = SameSiteMode.Lax;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                options.AccessDeniedPath = "/";
                options.LoginPath = "/Member/Login";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy => policy.Requirements.Add(new AdminRequirement(1)));
            });

            services.AddSingleton<IAuthorizationHandler, AdminHandler>();

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //throw new Exception("test fail to startup");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseStaticFiles();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStaticFiles(new StaticFileOptions()
                {
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
                    }
                });
            }

            // Shows UseCors with CorsPolicyBuilder. ( before any call to UseMvc )
            app.UseCors("AllowSpecificOrigin");

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();
            //add NLog.Web
            app.AddNLogWeb();
            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("NLogDb");
            LogManager.Configuration.Variables["configDir"] = "c:\\temp\\Logs";

            //app.UseRequestCulture();
            app.UseResponseCompression();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // ASP .NET CORE
        // https://docs.microsoft.com/en-us/aspnet/core/
        // https://docs.microsoft.com/en-us/aspnet/core/tutorials/
        // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page

        // ASP .NET CORE EF
        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext

        // NLog
        // https://damienbod.com/2016/08/17/asp-net-core-logging-with-nlog-and-microsoft-sql-server/
        // https://github.com/NLog/NLog.Web
        // https://github.com/NLog/NLog.Web/wiki/Getting-started-with-ASP.NET-Core-(csproj---vs2017)

        // Global exception handling
        // https://dusted.codes/error-handling-in-aspnet-core
        // https://blog.kloud.com.au/2016/03/23/aspnet-core-tips-and-tricks-global-exception-handling/

        // ASP.NET - Writing Clean Code in ASP.NET Core with Dependency Injection
        // https://msdn.microsoft.com/magazine/mt703433.aspx


        // in memory with async
        // https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core

        // https://stackoverflow.com/questions/42356411/how-to-design-a-repository-pattern-with-dependency-injection-in-asp-net-core-mvc
        // https://pioneercode.com/post/dependency-injection-logging-and-configuration-in-a-dot-net-core-console-app
        // https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core

        // client side
        // https://marketplace.visualstudio.com/items?itemName=EricLebetsamer.BootstrapSnippetPack
        // https://marketplace.visualstudio.com/items?itemName=RionWilliams.Glyphfriend2017
        // https://marketplace.visualstudio.com/items?itemName=MadsKristensen.BundlerMinifier

        // repo pattern with CRUD sample
        // https://social.technet.microsoft.com/wiki/contents/articles/36287.repository-pattern-in-asp-net-core.aspx
        // https://social.technet.microsoft.com/wiki/contents/articles/36510.asp-net-core-generic-repository-pattern.aspx

        // Validators
        // https://github.com/JeremySkinner/FluentValidation/wiki/i.-ASP.NET-Core-integration
        // http://cecilphillip.com/fluent-validation-rules-with-asp-net-core/

        // Scaffold-database from Package Manager console
        // Scaffold-DbContext "Server=DESKTOP-4CO0ANB;Database=GoK;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir KModels

        // Add-Migration MyFirstTable -C SchooldContext
        // Update-Database -C SchoolContext

        // hmac token
        // http://bitoftech.net/2014/12/15/secure-asp-net-web-api-using-api-key-authentication-hmac-authentication/
    }
}
