﻿using System;
using AdvertisingServer.Infrastructure.Extensions;
using AdvertisingServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisingServer
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddEntityFrameworkInMemoryDatabase();

            services.AddDbContextPool<MarketingDbContext>((serviceProvider, options) =>
            {
                options.UseInMemoryDatabase("MarketingDb");
                options.UseInternalServiceProvider(serviceProvider);
            });

            services.AddMarketingServices();

            services.RegisterSwaggerDoc();
            services.RegisterMapperConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Advertising API v1");
                c.RoutePrefix = String.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
;