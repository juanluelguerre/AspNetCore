using ElGuerre.AspNetCore.Cross.Exception.Filter;
using ElGuerre.AspNetCore.Cross.Exception.Filter.AspNetCoreNlog;
using ElGuerre.AspNetCore.Cross.Exception.Middleware;
using ElGuerre.AspNetCore.SampleApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace ElGuerre.AspNetCore.SampleApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var useFilter = Configuration.GetValue<bool>("ExceptionFilter");
            services.AddMvc(op =>
            {
                if (useFilter)
                {
                    op.Filters.Add<ExceptionFilter>();
                    op.Filters.Add<LogFilter>();
                }
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // services.AddScoped<LogFilter>();
            services.AddScoped<IValuesServices, ValuesServices>();

            #region Swagger

            // Add Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "Test For Exeption and Login",
                        Description = "API to expose Test For Exeption and Login",
                        TermsOfService = "Copyright (c) WLMS-IDB. All rights reserved."
                    }
                );

            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logFactory.AddDebug();
            }
            else
            {
                app.UseExceptionHandler();
            }

            var useMiddleware = Configuration.GetValue<bool>("ExceptionMiddleware");
            if (useMiddleware) app.UseExceptionMiddleware();
            
            //logFactory.AddConsole(); // Configured using NLog, (nlog.config file).
            logFactory.AddNLog();
            env.ConfigureNLog("nlog.config");
            //logFactory.AddApplicationInsights();

            app.UseMvc();

            #region Swagger

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Demo Api V1");
               });

            #endregion
        }
    }
}
