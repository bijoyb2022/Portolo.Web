using System;
using System.Runtime.Serialization;

namespace Portolo.Utility.Configuration
{
    public class ConfigurationSectionNotFoundException : Exception
    {
        public ConfigurationSectionNotFoundException()
        {
        }

        public ConfigurationSectionNotFoundException(string message)
            : base(message)
        {
        }

        public ConfigurationSectionNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ConfigurationSectionNotFoundException(string sectionKey, Type sectionType, Exception exception = null)
            : this($"A configuration section named {sectionKey} of type {sectionType.FullName} was not found.", exception)
        {
        }

        protected ConfigurationSectionNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
