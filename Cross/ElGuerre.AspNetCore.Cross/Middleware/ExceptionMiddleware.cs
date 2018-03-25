using ElGuerre.AspNetCore.Cross.Exception.Exception;
using ElGuerre.AspNetCore.Cross.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ElGuerre.AspNetCore.Cross.Exception.Middleware
{
    public class ExceptionMiddleware
    {
        readonly RequestDelegate _next;
        readonly IHostingEnvironment _env;
        readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, IHostingEnvironment env, ILoggerManager logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _env = env ?? throw new ArgumentException(nameof(env));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception e)
            {
                await HandleExceptionAsync(context, e, _env, _logger);
            }
        }

        static Task HandleExceptionAsync(HttpContext context, System.Exception exception, IHostingEnvironment env, ILoggerManager logger)
        {
            var message = String.Empty;
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            switch (exception)
            {
                case BusinessException ex:
                    code = HttpStatusCode.BadRequest;
                    message = $"Busines Exception: {ex.Message}";
                    break;
                case DataException ex:
                    code = HttpStatusCode.BadRequest;
                    message = $"DataException {ex.Message}";
                    break;
                case BaseException ex:
                    code = HttpStatusCode.BadRequest;
                    message = $"BaseException {ex.Message}";
                    break;
                case System.Exception ex:
                    code = HttpStatusCode.InternalServerError;
                    message = $"Exception: {ex.Message}";
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    message = $"Unexpected Error. {exception.Message}";
                    break;
            }

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            // logger.LogError(exception, message);
            logger.LogError(message);
            if (env.IsDevelopment()) logger.Debug(exception, message);
            //logger.Information(exception, message);

            var result = new ApiResponse() {
                IsValid = false,
                Message = env.IsDevelopment() ? $"{message}{exception.StackTrace}" : message
            }.ToJson();
            
            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
