using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReceivingService.DB;
using ReceivingService.Services;
using ReceivingService.Models;

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
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.Configure<DataStoreDatabaseSettings>(Configuration.GetSection(nameof(DataStoreDatabaseSettings)));
            services.AddScoped(typeof(IDataCollection<>), typeof(EntityCollection<>));
            services.AddSingleton<IDataStoreDatabaseSettings>(sp =>
                    sp.GetRequiredService<IOptions<DataStoreDatabaseSettings>>().Value);
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
