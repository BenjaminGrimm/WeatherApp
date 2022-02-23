using System.Runtime.Serialization;

namespace WeatherApp.Exceptions
{
    public class LocationNotFoundException : Exception
    {
        public LocationNotFoundException()
        {
        }

        public LocationNotFoundException(string? message) : base(message)
        {
        }

        public LocationNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LocationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
