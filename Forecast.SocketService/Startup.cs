using Forecast.Shared.Configuration;
using Forecast.Shared.Extensions;
using Forecast.SocketService.HostedService;
using Forecast.SocketService.Hub;
using Forecast.SocketService.Notification;
using Forecast.SocketService.ServiceConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Forecast.SocketService
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<INotifyService, ForecastNotification>();
            services.AddHostedService<ConsumerHostedService>();
            services.AddJsonOptions();
            services.AddSerilogServices();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseSignalR(builder => { builder.MapHub<ForecastHub>("/socket"); });
        }
    }
}