using Newtonsoft.Json.Linq;

namespace Stockexchange.Service
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";
        public string Code { get; set; }

        public HttpStatusCodeException(object status404NotFound, int statusCode)
        {
            this.StatusCode = statusCode;
        }


        public HttpStatusCodeException(int statusCode, string message, string Code = "0") : base(message)
        {
            this.ContentType = @"application/json";
            this.StatusCode = statusCode;
            this.Code = Code;
        }

        public HttpStatusCodeException(int statusCode, Exception inner, string Code = "0") : this(statusCode, inner.ToString(), Code) { }

        public HttpStatusCodeException(int statusCode, JObject errorObject, string Code = "0") : this(statusCode, errorObject.ToString(), Code)
        {
            this.ContentType = @"application/json";
        }
    }
}