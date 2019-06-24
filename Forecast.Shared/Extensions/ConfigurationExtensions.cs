using Forecast.Shared.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Forecast.Shared.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection UseMongoContext<T>(this IServiceCollection services) where T : BaseContext
        {
            services.AddScoped<T>();
            services.AddScoped<IDataCollection, EntityCollection>();
            services.AddSingleton<IClient, MongoDbClient>();
            return services;
        }
    }
}