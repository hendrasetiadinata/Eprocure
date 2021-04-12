using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ApplicationCore.Hosting
{
    public class ApiWebHostBuilder
    {
        public static IWebHostBuilder Init<TStartup>(string[] args, string[] urls) where TStartup : class
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                if (env.EnvironmentName != "Production" && urls.Any()) builder.UseUrls(urls);
            })
            .ConfigureServices(s => { s.AddSingleton(builder); })
            .UseStartup<TStartup>();

            return builder;
        }
    }
}
