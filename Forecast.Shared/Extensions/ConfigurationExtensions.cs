using Forecast.Shared.Configuration;
using Forecast.Shared.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        
        public static IServiceCollection AddJsonOptions(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.Configure<RabbitSettings>(settings => configuration.GetSection(nameof(RabbitSettings)));
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton<IRabbitSettings>(sp => 
                sp.GetRequiredService<IOptions<RabbitSettings>>().Value);
            return services;
        }
    }
}