using Forecast.Shared.Configuration;
using Forecast.Shared.Extensions;
using Forecast.SocketService.HostedService;
using Forecast.SocketService.Hub;
using Forecast.SocketService.Notification;
using Forecast.SocketService.ServiceConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddJsonOptions();
            services.AddSingleton<INotifyService, ForecastNotification>();
            services.AddHostedService<ConsumerHostedService>();
            services.AddSerilogServices();
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseCors("CorsPolicy");
            app.UseSignalR(builder =>
            {
                builder.MapHub<ForecastHub>("/socket");
            });
            app.UseMvc();
        }
    }
}