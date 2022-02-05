using System;
using System.Runtime.Serialization;

namespace DepRegAttributes
{
    public class DepRegAttributeException : Exception
    {
        public DepRegAttributeException() : base() { }
        protected DepRegAttributeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public DepRegAttributeException(string message) : base(message) { }
        public DepRegAttributeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
