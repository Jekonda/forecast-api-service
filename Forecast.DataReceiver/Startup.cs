using Forecast.DataReceiver.Configuration;
using Forecast.DataReceiver.DB;
using Forecast.DataReceiver.ServiceConfiguration;
using Forecast.DataReceiver.Services;
using Forecast.DataReceiver.Services.Client;
using Forecast.DataReceiver.Services.Messages;
using Forecast.DataReceiver.Services.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<AppDbContext>();
            services.AddScoped<ICrudRepository, CrudRepository>();
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.Configure<RabbitSettings>(Configuration.GetSection(nameof(RabbitSettings)));
            services.AddScoped<IDataCollection, EntityCollection>();
            services.AddTransient<IQueueSender, RabbitSender>();
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton<IRabbitSettings>(sp => 
                sp.GetRequiredService<IOptions<RabbitSettings>>().Value);
            services.AddSingleton<IClient, MongoDbClient>();
            services.AddSerilogServices();
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

            app.UseMvc();
        }
    }
}