namespace ElGuerre.AspNetCore.Cross.Exception.Exception
{
    public class DataException : System.Exception, IBaseException
    {
        public string AppName { get; set; }

        public DataException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public DataException(string message) : base(message)
        {
        }
    }
}
