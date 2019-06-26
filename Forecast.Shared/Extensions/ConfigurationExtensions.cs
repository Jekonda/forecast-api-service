using System;
using System.Text.RegularExpressions;
using Forecast.Shared.Configuration;
using Forecast.Shared.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

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
            services.Configure<RabbitSettings>(configuration.GetSection(nameof(RabbitSettings)));
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton<IRabbitSettings>(sp => 
                sp.GetRequiredService<IOptions<RabbitSettings>>().Value);
            return services;
        }
        
        public static IServiceCollection AddSerilogServices(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var db = sp.GetService<IDatabaseSettings>();
            var connectionString = db.ConnectionString;
            if(string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(nameof(connectionString));
            if (db.ConnectionString.Contains("?"))
            {
                var optionsString = new Regex(@"\?\S+").Match(db.ConnectionString).Value;
                connectionString = $"{db.ConnectionString.Replace(optionsString, "")}/{db.DatabaseName}{optionsString}";
            }
            else
            {
                connectionString += $"/{db.DatabaseName}";
            }
            var configuration = sp.GetRequiredService<IConfiguration>();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .WriteTo.MongoDB(connectionString, $"{AppDomain.CurrentDomain.FriendlyName}Logs", period: TimeSpan.FromMilliseconds(1))
                .CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }
    }
}