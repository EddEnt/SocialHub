
namespace Application.Core
{
    public class AppException(int statusCode, string exceptionMessage, string stackTrace = null)
    {
        public int StatusCode { get; set; } = statusCode;
        public string ExceptionMessage { get; set; } = exceptionMessage;
        public string StackTrace { get; set; } = stackTrace;
    }
}
