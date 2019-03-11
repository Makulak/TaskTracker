using System;
using System.Net;

namespace TaskTracker.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode ExceptionCode { get; }
        public string ExceptionMessage { get; }

        public string CompleteMessage => $"{(int)ExceptionCode} - {ExceptionMessage}";

        public RestException(HttpStatusCode exceptionCode, string exceptionMessage)
        {
            ExceptionCode = exceptionCode;
            ExceptionMessage = exceptionMessage;
        }
    }
}
