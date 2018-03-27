using ElGuerre.AspNetCore.Cross.Logging;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElGuerre.AspNetCore.Cross.Filter
{

    public class ControllerActionFilter : IAsyncActionFilter
    {
        private readonly ILogger _logger;

        public ControllerActionFilter(ILogger logger)
        {
            _logger = logger;
=======
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System;

namespace ElGuerre.AspNetCore.Cross.Filter
{
    public class LoggingActionFilter : IAsyncActionFilter
    {
        readonly ILoggerManager _logger;

        public LoggingActionFilter(ILoggerManager loggerManager)
        {
            _logger = loggerManager;
>>>>>>> 313d6603a85f9d8f0096d803c678a6dc1bd3f05f
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
<<<<<<< HEAD
            _logger.LogTrace("OnActionExecutionAsync - Before");

            LogInfo logInfo = new LogInfo();
            logInfo.StartTime = DateTime.UtcNow;
            //auditInfo.Type = "FUNC";
            logInfo.SeqType = "IN";
            logInfo.StatusCode = "000"; // ?????????

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                logInfo.SessionId = context.HttpContext.Session.Id;

                Claim name = ((ClaimsIdentity)context.HttpContext.User.Identity).FindFirst("name");
                if (name != null)
                {
                    logInfo.UserName = name.Value;
                }
                Claim preferredUsername = ((ClaimsIdentity)context.HttpContext.User.Identity).FindFirst("preferred_username");
                if (preferredUsername != null)
                {
                    logInfo.Email = preferredUsername.Value;
                }
            }

            logInfo.URI = context.HttpContext.Request.Path;
            logInfo.ModuleName = context.Controller.GetType().Module.Name;
            logInfo.Data = null;
            _logger.LogTrace(logInfo.ToString());

=======
            _logger.Debug("OnActionExecutionAsync - Before");

            var logInfo = new LogInfo
            {
                Id = Guid.NewGuid()
            };

            logInfo.StartDateTime = DateTime.UtcNow;
            logInfo.ServerName = Dns.GetHostName();
            logInfo.ServerIp = context.HttpContext.Connection.LocalIpAddress.ToString();
            // logInfo.RemoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

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

            _logger.LogAudit(logInfo);
>>>>>>> 313d6603a85f9d8f0096d803c678a6dc1bd3f05f

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();

            var nextContext = await next();

            if (nextContext.Exception == null)
            {
<<<<<<< HEAD
                _logger.LogDebug("OnActionExecutionAsync - After");

                stopwatch.Stop();

                logInfo.EndTime = logInfo.StartTime.Value.AddMilliseconds(stopwatch.ElapsedMilliseconds);
                logInfo.TimeElapsed = stopwatch.ElapsedMilliseconds;

                if (nextContext.Result != null && nextContext.Result is ObjectResult)
                {
                    var nextResult = nextContext.Result as ObjectResult;

                    //
                    // TODO: Save HTTP Information
                    //
                    // logInfo.Data = nextContext.HttpContext.Response.
                }

                logInfo.SeqType = "OUT";
                logInfo.Data = null;
                _logger.LogTrace(logInfo.ToString());
            }
        }
    }
}
=======
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
>>>>>>> 313d6603a85f9d8f0096d803c678a6dc1bd3f05f
