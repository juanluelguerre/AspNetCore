namespace ElGuerre.AspNetCore.Cross.Exception.Exception
{
    public class DataException : BaseException
    {
        public DataException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public DataException(string message) : base(message)
        {
        }
    }
}
