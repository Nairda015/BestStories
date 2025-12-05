using Microsoft.AspNetCore.Diagnostics;

namespace BestStories.Shared.Exceptions;

public class InvalidServesResponseException : Exception
{
    public InvalidServesResponseException() : base("Hacker-news API returned invalid response") { }
}

public class InvalidServesResponseExceptionHandler : IExceptionHandler
{
    private readonly ILogger<InvalidServesResponseExceptionHandler> _logger;

    public InvalidServesResponseExceptionHandler(ILogger<InvalidServesResponseExceptionHandler> logger)
        => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not InvalidServesResponseException invalidEx) return false;

        _logger.LogWarning(invalidEx, "Invalid response from external service");

        await ErrorWriter.WriteAsync(
            httpContext,
            StatusCodes.Status500InternalServerError,
            invalidEx.Message
        );

        return true;
    }
}