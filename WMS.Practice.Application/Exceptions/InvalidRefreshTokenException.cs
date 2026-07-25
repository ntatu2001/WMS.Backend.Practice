namespace WMS.Practice.Application.Exceptions
{
    [Serializable]
    public class InvalidRefreshTokenException : Exception
    {
        public InvalidRefreshTokenException() : base("The refresh token is invalid, expired or revoked.")
        {
        }

        public InvalidRefreshTokenException(string message) : base(message)
        {
        }

        public InvalidRefreshTokenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidRefreshTokenException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
        }
    }
}
