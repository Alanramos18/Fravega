using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fravega.Data
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register all the configuration of the context
        /// </summary>
        /// <param name="services"></param>
        public static void AddContextConfiguration(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IFravegaContext, FravegaContext>();
            services.AddScoped<ILogger<DbConfig>, Logger<DbConfig>>();
        }
    }
}
