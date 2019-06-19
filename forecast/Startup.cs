using forecast.Configuration;
using forecast.DB;
using forecast.SerivceConfiguration;
using forecast.Services;
using forecast.Services.Client;
using forecast.Services.Messages;
using forecast.Services.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace forecast
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