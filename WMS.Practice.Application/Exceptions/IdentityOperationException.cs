namespace WMS.Practice.Application.Exceptions
{
    [Serializable]
    public class IdentityOperationException : Exception
    {
        public List<string> Errors { get; } = new();

        public IdentityOperationException(IEnumerable<IdentityError> errors)
            : base("One or more identity operations failed.")
        {
            Errors = errors.Select(e => e.Description).ToList();
        }

        public IdentityOperationException(string message) : base(message)
        {
        }

        public IdentityOperationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IdentityOperationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
        }
    }
}
