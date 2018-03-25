namespace ElGuerre.AspNetCore.Cross.Exception.Exception
{
    public class BusinessException : BaseException
    {
        public BusinessException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public BusinessException(string message) : base (message)
        {
        }
    }
}
