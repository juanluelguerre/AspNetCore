using ElGuerre.AspNetCore.Cross.Logging;
using Microsoft.AspNetCore.Mvc;
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
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
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


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();

            var nextContext = await next();

            if (nextContext.Exception == null)
            {
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
