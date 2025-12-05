namespace BestStories.Shared.Exceptions;

public static class ErrorWriter
{
    public static async Task WriteAsync(
        HttpContext context,
        int statusCode,
        string errorMessage)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(errorMessage);
    }
}