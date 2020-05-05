using DevBridgeAPI.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace DevBridgeAPI.UseCases.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public IEnumerable<ErrorMessage> Errors { get; }

        public ApiException(HttpStatusCode statusCode, IEnumerable<ErrorMessage> errors)
            : this ($"Request failed with {errors.Count()} errors")
        {
            StatusCode = statusCode;
            Errors = errors;
        }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception innerException) : base(message, innerException) { }
        protected ApiException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
        public ApiException() : base() { }
    }
}