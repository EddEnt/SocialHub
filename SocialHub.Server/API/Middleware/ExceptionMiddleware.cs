using Application.Core;
using System.Net;
using System.Text.Json;

namespace SocialHub.Server.API.Middleware
{
    //This class is used to catch exceptions and return them as JSON
    //This is used in Program.cs with our middleware
    //  and gives more control over what happens when an exception is thrown
    public class ExceptionMiddleware(RequestDelegate requestDelegate,
        ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
    {
        private readonly RequestDelegate _requestDelegate = requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _hostEnvironment = hostEnvironment;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";                
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _hostEnvironment.IsDevelopment()
                    ? new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new AppException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
        
    }
}
