namespace ElGuerre.AspNetCore.Cross.Exception.Exception
{
    public class BaseException : System.Exception, IBaseException
    {
        public string AppName { get; set; }

        public BaseException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public BaseException(string message) : base(message)
        {
        }
    }
}