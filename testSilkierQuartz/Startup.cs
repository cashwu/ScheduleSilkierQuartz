using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using SilkierQuartz;
using testSilkierQuartz.Job;

namespace testSilkierQuartz
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
            services.AddRazorPages();
            services.AddSilkierQuartz();

            services.AddQuartzJob<HelloJob>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSilkierQuartz(new SilkierQuartzOptions
            {
                VirtualPathRoot = "/SilkierQuartz",
                UseLocalTime = true,
                DefaultDateFormat = "yyyy-MM-dd",
                DefaultTimeFormat = "HH:mm:ss"
            });

            app.UseQuartzJob<HelloJob>(new List<TriggerBuilder>
            {
                TriggerBuilder.Create()
                              .WithSimpleSchedule(a => a.WithIntervalInSeconds(3).RepeatForever()),
                TriggerBuilder.Create()
                              .WithSimpleSchedule(a => a.WithIntervalInSeconds(5).RepeatForever())
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}