using System.IO;
using System.Threading.Tasks;
using Demo.Backend.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace Demo.Backend
{
    internal static class Program
    {
        private static async Task Main()
        {
            // Our poor man's backend, demonstrating 4 "microservices" for

            await Task.WhenAll(
                // product prices
                BootStrap<StartupPrices>(5001),
                // product names
                BootStrap<StartupProducts>(5002),
                // product images
                BootStrap<StartupImages>(5003),
                // shoppping cart
                BootStrap<StartupCart>(5004)
            );            
        }

        private static Task BootStrap<T>(int port) where T : class
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<T>()
                .UseUrls($"http://+:{port}")
                .Build();

            return host.RunAsync();
        }
    }
}
