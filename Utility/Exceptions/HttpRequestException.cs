using System.Net;

namespace FinancialApplication.Utility.Exceptions
{
    public class HttpRequestException : System.Net.Http.HttpRequestException
    {
        public new HttpStatusCode StatusCode;

        public HttpRequestException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}