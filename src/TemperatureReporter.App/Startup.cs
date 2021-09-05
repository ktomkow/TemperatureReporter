﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using TemperatureReporter.App.Store;

namespace TemperatureReporter.App
{
    public class Startup
    {
        private readonly IWebHostEnvironment env;

        public IConfiguration Configuration { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>((options) =>
            {
                return Configuration.GetSection("Redis").Get<RedisConfiguration>();
            });

            if (env.IsDevelopment())
            {
                services.AddSingleton<IMeasurer, FakeMeasurer>();
            }
            else
            {
                services.AddSingleton<IMeasurer, Measurer>();
            }

            services.AddSingleton<MeasurementRedisCache>();
            services.AddSingleton<MeasurementGetter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
