using System;
<<<<<<< HEAD
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
=======
namespace ElGuerre.AspNetCore.Cross.Logging
{
    public class LogInfo : ICloneable
    {
        public Guid? Id { get; set; } = null;
        public int Seq { get; set; } = 0;
        public string SeqType { get; set; } = null;

        public DateTime? StartDateTime { get; set; } = null;
        public DateTime? EndDateTime { get; set; } = null;
        public long? TimeElapsed { get; set; } = null;

        //public string UserName { get; set; } = null;
        //public string Email { get; set; } = null;
        //public string SessionId { get; set; } = null;
        public string StatusCode { get; set; } = null;
        public string Uri { get; set; } = null;
        public string OperationType { get; set; } = null;
        public string OperationCode { get; set; } = null;

        public string Type { get; set; } = null;
        public string ModuleCode { get; set; } = null;
        public string ModuleType { get; set; } = null;
        public string ComponentType { get; set; } = null;
        public string ComponentCode { get; set; } = null;

        public string ServerName { get; set; } = null;
        public string ServerIp { get; set; } = null;

        public object Data { get; set; } = null;

        public object Clone()
        {
            return this.MemberwiseClone();
>>>>>>> 313d6603a85f9d8f0096d803c678a6dc1bd3f05f
        }
    }
}
