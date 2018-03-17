namespace ElGuerre.AspNetCore.Cross.Exception.Exception
{
    public class BusinessException : System.Exception
    {
        public string AppName { get; set; }

        public BusinessException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public BusinessException(string message) : base (message)
        {
        }
    }
}
