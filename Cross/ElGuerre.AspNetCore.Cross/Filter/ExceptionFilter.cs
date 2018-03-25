//using ElGuerre.AspNetCore.Cross.Exception.Exception;
//using ElGuerre.AspNetCore.Cross.Exception.Infrastructure;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.Logging;
//using System;

//namespace ElGuerre.AspNetCore.Cross.Exception.Filter
//{
//    public class ExceptionFilter : IExceptionFilter
//    {
//        private readonly IHostingEnvironment _env;
//        readonly ILogger<ExceptionFilter> _logger;

//        public ExceptionFilter(IHostingEnvironment env, ILogger<ExceptionFilter> logger)
//        {
//            _env = env;
//            _logger = logger;
//        }

//        public void OnException(ExceptionContext context)
//        {
//            String message = String.Empty;

//            switch (context.Exception)
//            {
//                case BusinessException ex:
//                    message = $"Busines Exception: {ex.Message}";
//                    break;
//                case DataException ex:
//                    message = $"DataException {ex.Message}";
//                    break;
//                case System.Exception ex:
//                    message = $"Exception: {ex.Message}";
//                    break;
//                default:
//                    break;
//            }

//            if (_env.IsDevelopment())
//            {
//                message = $"{message}{Environment.NewLine}{context.Exception.StackTrace}";
//            }

//            // https://stackify.com/nlog-guide-dotnet-logging/
//            // NLog.LogManager.GetLogger("logfile").Error(message);

//            var result = new ApiResponse<bool>() { IsValid = false, Data = false, Message = message };
//            context.Result = new OkObjectResult(result);

//            context.ExceptionHandled = true;
//        }
//    }
//}
