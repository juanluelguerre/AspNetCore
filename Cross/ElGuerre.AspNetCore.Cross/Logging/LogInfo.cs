using System;
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
        }
    }
}
