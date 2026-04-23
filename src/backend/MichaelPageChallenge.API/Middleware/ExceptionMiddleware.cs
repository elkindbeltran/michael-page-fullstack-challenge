namespace MichaelPageChallenge.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var apiError = new ApiErrorResponse();

        apiError.TraceId = context.TraceIdentifier;

        switch (exception)
        {
            case ValidationException validationEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                apiError.StatusCode = response.StatusCode;
                apiError.Message = "Validation error";

                apiError.Errors = validationEx.Errors
                    .Select(e => new
                    {
                        e.PropertyName,
                        e.ErrorMessage
                    });

                break;

            case NotFoundException notFoundEx:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                apiError.StatusCode = response.StatusCode;
                apiError.Message = notFoundEx.Message;
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                apiError.StatusCode = response.StatusCode;
                apiError.Message = "Internal server error";
                break;
        }

        var json = JsonSerializer.Serialize(apiError);

        await response.WriteAsync(json);
    }
}