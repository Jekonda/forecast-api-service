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
        
        public static IServiceCollection MapJsonOptions<TI, TC>(this IServiceCollection services) where TC : class, TI, new() where TI : class
        {
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            services.Configure<TC>(configuration.GetSection(typeof(TC).Name));
            services.AddSingleton<TI>(sp => sp.GetRequiredService<IOptions<TC>>().Value);
            return services;
        }
        
        [Obsolete("AddMongoSerilogServices is deprecated, use AddSerilogConsoleService")]
        public static IServiceCollection AddMongoSerilogServices(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var db = sp.GetService<IDatabaseSettings>();
            if(db == null)
                throw new ArgumentNullException(nameof(IDatabaseSettings));
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
        
        public static IServiceCollection AddSerilogConsoleService(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var configuration = sp.GetRequiredService<IConfiguration>();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }
    }
}