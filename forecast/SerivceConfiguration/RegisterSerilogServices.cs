using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReceivingService.Models;
using Serilog;

namespace ReceivingService.SerivceConfiguration
{
    public static class RegisterSerilogServices
    {
        public static IServiceCollection AddSerilogServices(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var db = sp.GetService<IDatabaseSettings>();

            var connectionString = db.ConnectionString;
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
                .WriteTo.MongoDB(connectionString, "Logs", period: TimeSpan.FromMilliseconds(1))
                .CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }
    }
}