using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shared.Exceptions;

public class CustomExceptionHandler(
    ILogger<CustomExceptionHandler> logger
) : IExceptionHandler
{

    private Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers = [];
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(
            "Error Message: {exectionMessage}, time of occurrence: {time}",
            exception.Message, DateTime.UtcNow
        );
        _exceptionHandlers = new() {
            {typeof(ValidationException), HandleValidationException},
            {typeof(NotFoundException), HandleNotFoundException},
            {typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException},
            {typeof(ForbiddenAccessException), HandleForbiddenAccessException},
            {typeof(JsonException), HandleJsonException},
            {typeof(BadHttpRequestException), HandleBadRequestException},
        };
        var exceptionType = exception.GetType();
        foreach (var exType in _exceptionHandlers.Keys)
        {
            if (exType.IsAssignableFrom(exceptionType))
            {
                if (_exceptionHandlers.TryGetValue(exType, out Func<HttpContext, Exception, Task>? value))
                {
                    await value(httpContext, exception);
                    return true;
                }
            }
        }
        await HandleInternalServerErrorException(httpContext, exception);
        return true;
    }


    private async Task HandleJsonException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = ex.Message
        });
    }

    private async Task HandleBadRequestException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = ex.Message
        });
    }

    private async Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        var exception = (ValidationException)ex;
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails(exception.Errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        });
    }

    private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = ex.Message
        });
    }

    private async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Title = "Unauthorized",
            Detail = ex.Message
        });
    }
    private async Task HandleForbiddenAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            Title = "Forbidden",
            Detail = ex.Message
        });
    }
    private async Task HandleInternalServerErrorException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "An error occurred while processing your request.",
            Detail = ex.Message
        });
    }
}
