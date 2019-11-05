﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Achivement_2.Models;
using Achivement_2.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Achivement_2
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<NumberstoreDatabaseSettings>(
                Configuration.GetSection(nameof(NumberstoreDatabaseSettings))
            );

            services.AddSingleton<INumberstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<NumberstoreDatabaseSettings>>().Value);

            services.AddTransient<IRepository<NumberEntity>, NumberRepository>();

            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ValueController}");
            });
        }
    }
}
