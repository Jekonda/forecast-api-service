using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReceivingService.DB;
using ReceivingService.Services;
using ReceivingService.Models;
using ReceivingService.Services.Client;
using ReceivingService.Services.Repository;

namespace ReceivingService
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
            services.AddScoped<MongoContext>();
            services.AddScoped<ICrudRepository, CrudRepository>();
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddScoped<IDataCollection, EntityCollection>();
            services.AddSingleton<IDatabaseSettings>(sp =>
                    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton<IClient, MongoDbClient>();
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
