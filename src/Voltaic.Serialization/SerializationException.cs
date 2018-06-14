using System;

namespace Voltaic.Serialization
{
    public class SerializationException : Exception
    {
        public SerializationException()
            : base("Serialization failed")
        {
        }
        public SerializationException(string message)
            : base(message)
        {
        }
    }
}
