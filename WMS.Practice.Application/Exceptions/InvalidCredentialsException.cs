namespace WMS.Practice.Application.Exceptions
{
    [Serializable]
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("The username or password is incorrect.")
        {
        }

        public InvalidCredentialsException(string message) : base(message)
        {
        }

        public InvalidCredentialsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidCredentialsException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
        }
    }
}
