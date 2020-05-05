using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DevBridgeAPI.UseCases.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; }
        public EntityNotFoundException(string message, Type entityType) : this (message)
        {
            EntityType = entityType;
        }
        public EntityNotFoundException() : base() { }
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
     
    }
}