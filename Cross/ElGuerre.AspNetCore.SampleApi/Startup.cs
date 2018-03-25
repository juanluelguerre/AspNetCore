using ElGuerre.AspNetCore.Cross.Exception.Middleware;
using ElGuerre.AspNetCore.Cross.Filter;
using ElGuerre.AspNetCore.Cross.Logging;
using ElGuerre.AspNetCore.SampleApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
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
            services.AddMvc(op =>
            {
                // op.Filters.Add<ExceptionFilter>();
                // op.Filters.Add<LogFilter>();
                op.Filters.Add<JsonResponseFilter>();
                op.Filters.Add<LoggingActionFilter>();
            });

            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IValuesServices, ValuesServices>();

            #region Swagger

            // Add Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "API Sample",
                        Description = "API Sample",
                        TermsOfService = "Copyright (c) ElGuerre.com. All rights reserved."
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

            app.UseExceptionMiddleware();

            // logFactory.AddConsole(); // Configured using NLog, (nlog.config file).
            logFactory.AddNLog();
            // logFactory.AddApplicationInsights();

            app.UseMvc();

            #region Swagger

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"/swagger/v1/swagger.json", "API Sample V1");
               });

            #endregion
        }
    }
}
