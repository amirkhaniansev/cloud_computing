using System;

namespace LivescoreAPI.Exceptions
{
    public class RequestException : Exception
    {
        public int HttpStatusCode { get; set; }

        public RequestException(string message, int httpStatusCode) : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
        }
    }
}
