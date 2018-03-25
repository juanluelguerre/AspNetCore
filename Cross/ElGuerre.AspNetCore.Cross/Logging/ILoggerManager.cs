
namespace ElGuerre.AspNetCore.Cross.Logging
{
    public interface ILoggerManager
    {
        void LogInfo(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, params object[] args);
        void LogAudit(LogInfo info);
        void Trace(string message, params object[] args);
        void Debug(string message, params object[] args);
        void Debug(System.Exception ex, string message);
        void Debug(System.Exception ex);
    }
}
