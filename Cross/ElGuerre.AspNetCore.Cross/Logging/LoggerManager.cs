using System;
using Microsoft.Extensions.Logging;

namespace ElGuerre.AspNetCore.Cross.Logging
{
    public class LoggerManager : ILoggerManager
    {
        ILogger _loggerError;
        ILogger _loggerAudit;

        public LoggerManager(ILoggerFactory loggerFactory)
        {
            _loggerError = loggerFactory.CreateLogger("ElGuerre.Log");
            _loggerAudit = loggerFactory.CreateLogger("ElGuerre.Audit");
        }

        public void LogAudit(LogInfo info)
        {
            _loggerAudit.LogTrace(Newtonsoft.Json.JsonConvert.SerializeObject(info));
        }

        public void LogError(string message, params object[] args)
        {
            if (_loggerError.IsEnabled(LogLevel.Error))
                _loggerError.LogError(message, args);
        }

        public void LogInfo(string message, params object[] args)
        {
            if (_loggerError.IsEnabled(LogLevel.Information))
                _loggerError.LogError(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            if (_loggerError.IsEnabled(LogLevel.Warning))
                _loggerError.LogError(message, args);

        }

        public void Trace(string message, params object[] args)
        {
            if (_loggerError.IsEnabled(LogLevel.Trace))
                _loggerError.LogTrace(message, args);
        }

        public void Debug(string message, params object[] args)
        {
            if (_loggerError.IsEnabled(LogLevel.Debug))
                _loggerError.LogDebug(message, args);
        }

        public void Debug(System.Exception ex, string message)
        {
            Debug(ex.StackTrace, message);
        }

        public void Debug(System.Exception ex)
        {
            Debug(ex.StackTrace, ex.Message);
        }
    }
}
