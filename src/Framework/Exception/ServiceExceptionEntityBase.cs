using System;
using System.Runtime.Serialization;

namespace Portolo.Framework.Exception
{
    [DataContract]
    public abstract class ServiceExceptionEntityBase
    {
        public ServiceExceptionEntityBase(System.Exception ex)
        {
            this.Source = ex.Source;
            this.Message = ex.Message;
            this.StackTrace = ex.StackTrace;
        }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string RequestParameter { get; set; }
    }
}