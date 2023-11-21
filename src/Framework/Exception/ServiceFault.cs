using System;
using System.Runtime.Serialization;

namespace Portolo.Framework.Exception
{
    [DataContract(Name = "ServiceFault", Namespace = "Portolo.Framework.Exception")]
    public class ServiceFault
    {
        public ServiceFault()
        {
        }

        public ServiceFault(System.Exception regularException, string requestJson = "", string additionalInfo = "")
        {
            this.Source = regularException.Source;
            this.Message = string.Format("TMS Service Failure Reason: {0}", regularException.Message);
            this.StackTrace = regularException.StackTrace;
            this.ServerTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            this.RequestJson = requestJson;
            this.AdditionalInfo = additionalInfo;
        }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        public string RequestJson { get; set; }

        [DataMember]
        public string AdditionalInfo { get; set; }

        [DataMember]
        public string ServerTime { get; set; }
    }
}