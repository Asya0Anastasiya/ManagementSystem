namespace DocumentServiceApi.Exceptions
{
    public class InternalException : Exception
    {
        public InternalException(string message = "") : base(message) { }
    }
}
