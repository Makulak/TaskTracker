using System.Net;

namespace TaskTracker.Exceptions
{
    class ServerResponseException : RestException
    {
        public override string CompleteMessage => ExceptionMessage;

        public ServerResponseException(HttpStatusCode exceptionCode, string exceptionMessage) : base(exceptionCode, exceptionMessage)
        {
        }

        public ServerResponseException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}
