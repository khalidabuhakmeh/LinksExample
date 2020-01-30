using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinksExample.Enrichers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LinksExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // we need you MVC!
            services.AddHttpContextAccessor();

            // We also have to register the object twice by type (use AutoFac)
            services
                .AddScoped<WeatherForecastEnricher>()
                .AddScoped<IEnricher, WeatherForecastEnricher>();
            
            services.AddScoped<IEnricher, WeatherForecastsEnricher>();

            services.AddScoped<RepresentationEnricher>();
            
            services.AddControllers(cfg =>
            {
                cfg.Filters.Add<RepresentationEnricher>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}