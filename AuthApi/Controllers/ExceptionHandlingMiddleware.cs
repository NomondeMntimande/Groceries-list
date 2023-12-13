using Newtonsoft.Json;
using System.Net;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Continue processing the request
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An unhandled exception occurred.");

            // Handle the exception and send a custom response
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            Success = false,
            ErrorMessage = "An unexpected error occurred."
            // Optionally, you can include additional details or log the exception ID for tracking
        }));
    }
}
