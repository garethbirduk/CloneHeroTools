using CloneHeroLibrary;
using Gradient.BuilderExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gradient.
{
    public class Program
    {
        /// <summary>
        /// Main application
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            using IServiceScope serviceScope = CreateDefaultBuilder(args)
                .BuildConfiguration()
                .AddService<CloneHeroHelper>()
                .Build()
                .Services
                .CreateScope();
            await serviceScope.ServiceProvider
                .GetRequiredService<CloneHeroHelper>()
                .RunAsync();
        }

        private static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args);
        }
    }
}