using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Steeltoe.Extensions.Logging;
using Steeltoe.Management.Endpoint;

namespace dotnetservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
            .Build()
            .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, builder) => builder.AddDynamicConsole())
                .UseStartup<Startup>()
                .AddAllActuators();
            return builder;
        }
    }
}
