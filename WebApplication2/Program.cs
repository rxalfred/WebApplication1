using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ContosoUniversity.Model.Data;
using ContosoUniversity.Model.CoreTestModel;
using System.Net;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SchoolContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 5000);
                    //options.Listen(IPAddress.Loopback, 5001, listenOptions =>
                    //{
                    //    listenOptions.UseHttps("testCert.pfx", "testPassword");
                    //})
                    ;
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.ConfigureAppConfiguration((hostingContext, config) =>
                //{
                //    var env = hostingContext.HostingEnvironment;
                //    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                //    config.AddEnvironmentVariables();
                //})
                //.ConfigureLogging((hostingContext, logging) =>
                //{
                //    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                //    logging.AddConsole();
                //    logging.AddDebug();
                //})
                .CaptureStartupErrors(true)
                .UseSetting("detailedErrors", "true")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}


// Check List
// 1. Logging - OK
// 2. EF - OK
// 3. Validation - OK
// 4. Identity
// 5. Localization - OK
