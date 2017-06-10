using System;

namespace HashtagAggregatorTwitter.Service.Infrastructure.Exception
{
    public class ImmutableException : System.Exception
    {
        public ImmutableException()
        {
        }

        public ImmutableException(string message)
            : base(message)
        {
        }

        public ImmutableException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
