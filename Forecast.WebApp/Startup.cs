using AspNetCore.Security.Jwt;
using Forecast.Shared.Configuration;
using Forecast.Shared.Extensions;
using Forecast.WebApp.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Swagger;

namespace Forecast.WebApp
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Forecast API", Version = "v1" });
            });
            services.MapJsonOptions<IDatabaseSettings, DatabaseSettings>();
            services.AddSecurity<Authenticator>(Configuration, true);
//            services.AddSingleton<IUserStore<MongoIdentityUser>>(provider =>
//            {
//                var options = provider.GetService<IDatabaseSettings>();
//                var client = new MongoClient(options.ConnectionString);
//                var database = client.GetDatabase(options.DatabaseName);
//                return MongoUserStore<MongoIdentityUser>.CreateAsync(database).GetAwaiter().GetResult();
//            });
            //services.AddIdentity<MongoIdentityUser>()
              //  .AddDefaultTokenProviders();
            services.AddMvc()
                .AddSecurity()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "XXX API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSecurity(true);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}