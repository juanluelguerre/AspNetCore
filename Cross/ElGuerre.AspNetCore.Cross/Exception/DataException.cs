namespace ElGuerre.AspNetCore.Cross.Exception.Exception
{
<<<<<<< HEAD
    public class DataException : System.Exception, IBaseException
=======
    public class DataException : BaseException
>>>>>>> 313d6603a85f9d8f0096d803c678a6dc1bd3f05f
    {
        public DataException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public DataException(string message) : base(message)
        {
        }
    }
}
