using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Immortals
{
    class InvalidClickException : System.ApplicationException
    {
        public InvalidClickException() {}
        public InvalidClickException(string message) : base(message) {}
        public InvalidClickException(string message, System.Exception inner) : base(message, inner) { }

        // Constructor needed for serialization
        // when exception propagates from a remoting server to the client.
        // No idea what this means, but msdn suggested i put this in so I will.
        // TODO: learn why/if I need this
        protected InvalidClickException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context){ }
    }
}
