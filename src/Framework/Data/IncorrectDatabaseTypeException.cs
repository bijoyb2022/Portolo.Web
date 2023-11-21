using System;
using System.Runtime.Serialization;

namespace Portolo.Framework.Data
{
    [Serializable]
    internal class IncorrectDatabaseTypeException : Exception
    {
        public IncorrectDatabaseTypeException()
        {
        }

        public IncorrectDatabaseTypeException(string message)
            : base(message)
        {
        }

        public IncorrectDatabaseTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected IncorrectDatabaseTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}