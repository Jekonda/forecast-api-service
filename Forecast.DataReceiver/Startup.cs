using Forecast.DataReceiver.Context;
using Forecast.DataReceiver.Services.Messages;
using Forecast.DataReceiver.Services.Repository;
using Forecast.Shared.Configuration;
using Forecast.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forecast.DataReceiver
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
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.MapJsonOptions<IDatabaseSettings, DatabaseSettings>();
            services.MapJsonOptions<IRabbitSettings, RabbitSettings>();
            services.AddScoped<ICrudRepository, CrudRepository>();
            services.AddTransient<IQueueSender, RabbitSender>();
            services.UseMongoContext<AppDbContext>();
            services.AddSerilogConsoleService();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors();
            app.UseMvc();
        }
    }
}