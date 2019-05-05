using DebugSourceLink.Abstract;
using DebugSourceLink.Filters;
using DebugSourceLink.Impl;
using DebugSourceLink.Middlewares;
using DebugSourceLink.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DebugSourceLink
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
            var testConfigurationValue = Configuration["Logging:LogLevel:Default"];
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            /*InvalidOperationException: Cannot resolve scoped service 'DebugSourceLink.Abstract.IAmateurPoetry' from root provider.
             微软ASP.NETCore DI中root service provider 只管理生命周期为Singleton的service，scoped的service在对应scope provider中管理
             同一个request被处理的过程中，scoped类型的service都是同一个实例*/
            services.AddSingleton<IAmateurPoetry, Poetry>();
            services.AddScoped<IProfessionalPoetry, Poetry>();
            services.AddTransient<TestActionFilter>();

            //services.AddHostedService<TimedBackgroundServiceOne>();
            //services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, TimedBackgroundServiceOne>();
            services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, TimedBackgroundServiceTwo>();

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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            /*为什么在app.UseMvc()之前注入定制中间件可以使用中间件，在app.UseMvc()之后注入的中间件不生效？*/
            app.UseCustomAuthorizationMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseCustomLoggingMiddleware();
        }
    }
}
