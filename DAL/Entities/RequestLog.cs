using System;

namespace DAL.Entities
{
    public class RequestLog : IEntity
    {
        public virtual long Id { get; set; }
        public virtual string ServiceName { get; set; }
        public virtual string MethodName { get; set; }
        public virtual string RequestData { get; set; }
        public virtual string ResponseData { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual int DurationMs { get; set; }
        public virtual bool Success { get; set; }
        public virtual string Message { get; set; }
    }
}
