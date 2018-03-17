////
//// https://stackoverflow.com/questions/47181356/c-sharp-dotnet-core-middleware-wrap-response
////
//// IMPORTANT: Not work using Swagger !!!!
////
//using ElGuerre.AspNetCore.Cross.Exception.Infrastructure;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;

//namespace ElGuerre.AspNetCore.Cross.Exception.Middleware
//{
//    public class ResponseWrapperMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IHostingEnvironment _env;
//        private readonly ILogger<ExceptionMiddleware> _logger;


//        public ResponseWrapperMiddleware(RequestDelegate next, IHostingEnvironment env, ILoggerFactory loggerFactory)
//        {
//            _next = next ?? throw new ArgumentNullException(nameof(next));
//            _env = env ?? throw new ArgumentException(nameof(env));
//            _logger = loggerFactory?.CreateLogger<ExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            //Hold on to original body for downstream calls
//            Stream originalBody = context.Response.Body;
//            try
//            {
//                string responseBody = null;
//                using (var memStream = new MemoryStream())
//                {
//                    //Replace stream for upstream calls.
//                    context.Response.Body = memStream;
//                    //continue up the pipeline
//                    await _next(context);
//                    //back from upstream call.

//                    if (! context.Response.ContentType.Contains("json")) return;

//                    //memory stream now hold the response data
//                    //reset position to read data stored in response stream
//                    memStream.Position = 0;
//                    responseBody = new StreamReader(memStream).ReadToEnd();
//                }//dispose of previous memory stream.
//                 //lets convert responseBody to something we can use
//                var data = JsonConvert.DeserializeObject(responseBody);
//                //create your wrapper response and convert to JSON
//                var json = new ApiResponse()
//                {
//                    IsValid = true,
//                    Data = data,
//                    Message = null,
//                }.ToJson();
//                //convert json to a stream
//                var buffer = Encoding.UTF8.GetBytes(json);
//                using (var output = new MemoryStream(buffer))
//                {
//                    await output.CopyToAsync(originalBody);
//                }//dispose of output stream
//            }
//            finally
//            {
//                //and finally, reset the stream for downstream calls
//                context.Response.Body = originalBody;
//            }
//        }
//    }

//    public static class UseResponseWrapperMiddlewareExtensions
//     {
//        public static IApplicationBuilder UseResponseWrapperMiddleware(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<ResponseWrapperMiddleware>();
//        }
//    }
//}
