using System;
namespace ElGuerre.AspNetCore.Cross.Exception
{
    public interface IBaseException
    {
        string AppName { get; set; }
    }
}
