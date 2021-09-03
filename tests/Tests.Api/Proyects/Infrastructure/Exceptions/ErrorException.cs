using System;
using System.Net;

namespace Tests.Infrastructure
{
    public class ErrorException : Exception
    {
        public ErrorException(HttpStatusCode code, object message = default!)
        {
            Code = code;
            Message = message;
        }

        public HttpStatusCode Code { get; }

        public new object Message { get; }
    }
}