namespace ElGuerre.AspNetCore.Cross.Exception.Exception
{
<<<<<<< HEAD
    public class BusinessException : System.Exception, IBaseException
=======
    public class BusinessException : BaseException
>>>>>>> 313d6603a85f9d8f0096d803c678a6dc1bd3f05f
    {
        public BusinessException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public BusinessException(string message) : base (message)
        {
        }
    }
}
