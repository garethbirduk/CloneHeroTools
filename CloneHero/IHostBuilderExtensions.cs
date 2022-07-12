using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gradient.BuilderExtensions
{
    /// <summary>
    /// Helper extensions for building the app
    /// </summary>
    public static class IHostBuilderExtensions
    {
        /// <summary>
        /// Add standard configuration options
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder BuildConfiguration(this IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(app =>
            {
                app.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                app.AddEnvironmentVariables();
            });
            return builder;
        }

        /// <summary>
        /// Add a singleton to the services
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder AddService<TService>(this IHostBuilder builder)
            where TService : class
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<TService>();
            });
            return builder;
        }
    }
}