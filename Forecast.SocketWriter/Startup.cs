using forecast.Configuration;
using forecast.SerivceConfiguration;
using Forecast.SocketWriter.HostedService;
using Forecast.SocketWriter.Hub;
using Forecast.SocketWriter.Notification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Forecast.SocketWriter
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
            services.AddScoped<INotifyService, ForecastNotify>();
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.Configure<RabbitSettings>(Configuration.GetSection(nameof(RabbitSettings)));
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton<IRabbitSettings>(sp => 
                sp.GetRequiredService<IOptions<RabbitSettings>>().Value);
            services.AddSerilogServices();//todo create json settings
            services.AddHostedService<ConsumerHostedService>();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSignalR(builder => { builder.MapHub<ForecastHub>("/socket"); });
            app.UseRouting();
        }
    }
}