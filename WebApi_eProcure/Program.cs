using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ApplicationCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using Microsoft.Extensions.Configuration;

namespace WebApi_eProcure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                            .AddJsonFile(environment != Environments.Development ? "appsettings.json" : "appsettings.Development.json")
                            .Build();

            // Add seriLog
            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               //.WriteTo.Console()
               .ReadFrom.Configuration(config)
               .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args).UseSerilog()
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
    }
}
