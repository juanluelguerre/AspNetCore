using System;
using System.Reflection;
using System.Text;

namespace ElGuerre.AspNetCore.Cross.Logging
{
    [Serializable]
    public class LogInfo
    {
        public Guid? Id => Guid.NewGuid();
        public int Seq { get; set; } = 0;
        public string SeqType { get; set; } = null;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;        
        // public string Type { get; set; } = null;
        public string ModuleName { get; set; } = null;
        public string MethodName { get; set; } = null;        
        public long? TimeElapsed { get; set; } = null;
        public string UserName { get; set; } = null;
        public string Email { get; set; } = null;
        // public string UserProfile { get; set; } = null;        
        public string ServerName { get; set; } = null;
        public string ServerIp { get; set; } = null;
        // public string RemoteIp { get; set; } = null;
        public string SessionId { get; set; } = null;
        public string StatusCode { get; set; } = null;
        public string URI { get; set; } = null;
        public object Data { get; set; } = null;

        public override string ToString()
        {
            const char SEP = '|';

            StringBuilder sb = new StringBuilder();
            var props =this.GetType().GetMembers(BindingFlags.Public | BindingFlags.DeclaredOnly);        
            foreach (var p in props)            
                sb.Append($"{p.Name}{SEP}");
            return sb.ToString().TrimEnd(SEP);
        }
    }
}
