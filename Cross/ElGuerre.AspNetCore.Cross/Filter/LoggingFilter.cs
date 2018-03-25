using System;
using ElGuerre.AspNetCore.Cross.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace ElGuerre.AspNetCore.Cross.Filter
{
    public class LoggingActionFilter : IAsyncActionFilter
    {
        readonly ILoggerManager _logger;

        public LoggingActionFilter(ILoggerManager loggerManager)
        {
            _logger = loggerManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.Debug("OnActionExecutionAsync - Before");

            var logInfo = new LogInfo
            {
                Id = Guid.NewGuid()
            };


            DateTime dateTime = DateTime.UtcNow;

            logInfo.StartDateTime = dateTime;

            logInfo.ServerName = Dns.GetHostName();

            //IPHostEntry ipHostInfo = Dns.GetHostEntry(logInfo.ServerName);
            //IPAddress[] ipv4Addresses = Array.FindAll(Dns.GetHostEntry(string.Empty).AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);
            //logInfo.ServerIp = ipv4Addresses[1].ToString();

            logInfo.ServerIp = context.HttpContext.Connection.LocalIpAddress.ToString();
            // logInfo.RemoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

            // string httpClientIp = context.HttpContext.Request.Headers["HTTP_CLIENT_IP"];
            // var connectionFeature = context.HttpContext.Features.Get<IHttpConnectionFeature>();
            // string ip;
            // if (connectionFeature != null)
            // {
            //     ip = connectionFeature.RemoteIpAddress.ToString();
            // }

            var request = context.HttpContext.Request;
            logInfo.Uri = request.Path;
            logInfo.StatusCode = context.HttpContext.Response.StatusCode.ToString();

            // HTTP Request parser
            using (var bodyReader = new StreamReader(request.Body))
            {
                var body = bodyReader.ReadToEnd()?? String.Empty;
                logInfo.Data = JsonConvert.SerializeObject(
                    new { request.Headers, request.ContentType, request.Path, request.Protocol, request.Method, Body = body });
            }

            logInfo.ModuleCode = context.Controller.GetType().Module.Name;
            logInfo.ModuleType = context.Controller.GetType().Module.Name.Split('.').First();
            logInfo.ComponentCode = context.Controller.GetType().Name;
            //logInfo.CompCode = controllerActionDescriptor.ControllerName;
            logInfo.ComponentType = context.Controller.GetType().Namespace.Split('.').Last();

            ControllerActionDescriptor controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            logInfo.OperationCode = controllerActionDescriptor.ActionName;
            logInfo.OperationType = context.HttpContext.Request.Method;

 
            // ParameterInfo[] parameterInfo = controllerActionDescriptor.MethodInfo.GetParameters();
            //if (parameterInfo.Count() > 1)
            //{
            //logInfo.EventType = string.Join(",", parameterInfo.Select(p => p.GetType().ToGenericTypeString()));
            //logInfo.EventCod = string.Join(",", parameterInfo.Select(p => p.Name));
            //}
            //else if (parameterInfo.Count() == 1)
            //{
            //    auditInfo.EventType = parameterInfo[0].GetType().ToGenericTypeString();
            //    auditInfo.EventCod = parameterInfo[0].Name;
            //}

            _logger.LogAudit(logInfo);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();

            var nextContext = await next();

            if (nextContext.Exception == null)
            {
                _logger.Debug("OnActionExecutionAsync - After");

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;

                LogInfo logInfoResp = logInfo.Clone() as LogInfo;
                logInfoResp.ModuleCode = null;
                logInfoResp.ModuleType = null;
                logInfoResp.ComponentCode = null;
                logInfoResp.ComponentType = null;
                logInfoResp.OperationCode = null;
                logInfoResp.OperationType = null;

                logInfo.EndDateTime = ((DateTime)logInfo.StartDateTime).AddMilliseconds(stopwatch.ElapsedMilliseconds);
                logInfo.TimeElapsed = logInfo.TimeElapsed = stopwatch.ElapsedMilliseconds;

                if (nextContext.Result != null && nextContext.Result is ObjectResult)
                {
                    var nexValue = ((ObjectResult)(nextContext.Result)).Value;
                    // HTTP Response parser
                    logInfo.Data = JsonConvert.SerializeObject(
                        new { request.Headers, request.ContentType, request.Path, request.Protocol, request.Method, Body = nexValue });
                }

                _logger.LogAudit(logInfo);
            }
        }
    }
}