using System.Net;

namespace FinancialApplication.Utility.Exceptions
{
    public class UnauthorizedException : HttpRequestException
    {
        public UnauthorizedException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }

        public UnauthorizedException(string message) : base(message, HttpStatusCode.Unauthorized)
        {
        }
    }
}