using Microsoft.AspNetCore.Diagnostics;

namespace BestStories.Shared.Exceptions;

public class ServesUnavailableException : Exception
{
    public ServesUnavailableException() : base("Hacker-news API returned unsuccessful status") { }
}


public class ServesUnavailableExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger;

    public ServesUnavailableExceptionHandler(ILogger<DefaultExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "External service unavailable");

        await ErrorWriter.WriteAsync(
            httpContext,
            StatusCodes.Status500InternalServerError,
            exception.Message
        );

        return true; 
    }
}