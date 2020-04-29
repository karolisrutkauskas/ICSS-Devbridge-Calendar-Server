using DevBridgeAPI.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DevBridgeAPI.UseCases.Exceptions
{
    [Serializable]
    public class ValidationFailedException : Exception
    {
        public ValidationInfo ValidationInfo { get; }
        public ValidationFailedException(string message) : base(message) { }
        public ValidationFailedException(string message, Exception innerException) : base(message, innerException) { }
        protected ValidationFailedException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
        public ValidationFailedException(ValidationInfo validationInfo) : this(string.Join("; ", validationInfo.ErrorMessages))
        {
            ValidationInfo = validationInfo;
        }
        public ValidationFailedException() : base() { }
    }
}