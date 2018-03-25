using System;
using System.Collections.Generic;

namespace ElGuerre.AspNetCore.Cross.Logging
{
    public class HttpInfo
    {
        public int? StatusCode { get; set; } = null;
        public string Protocol { get; set; }
        public string Method { get; set; }
        public IDictionary<string, object> Headers { get; set; }
        public IDictionary<string, string> Cookies { get; set; }
        public IDictionary<string, string> Form { get; set; }
        public object Body { get; set; }
        public string QueryString { get; set; }
    }
}