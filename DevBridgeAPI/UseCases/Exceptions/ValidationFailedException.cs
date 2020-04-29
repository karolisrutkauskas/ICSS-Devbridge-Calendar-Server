using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DevBridgeAPI.UseCases.Exceptions
{
    [Serializable]
    public class UniqueFieldException : Exception
    {
        public UniqueFieldException(string message) : base(message) { }
        public UniqueFieldException(string message, Exception innerException) : base(message, innerException) { }
        protected UniqueFieldException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
        public UniqueFieldException() : base() { }
    }
}