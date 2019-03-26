
namespace TaskTracker.Exceptions
{
    class ServerResponseException : RestException
    {
        public override string CompleteMessage => ExceptionMessage;

        public ServerResponseException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}
