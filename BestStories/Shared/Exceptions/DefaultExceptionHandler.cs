using Microsoft.AspNetCore.Diagnostics;

namespace BestStories.Shared.Exceptions;

public class DefaultExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger;

    public DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled server error");

        await ErrorWriter.WriteAsync(
            httpContext,
            StatusCodes.Status500InternalServerError,
            exception.Message
        );

        return true; 
    }
}