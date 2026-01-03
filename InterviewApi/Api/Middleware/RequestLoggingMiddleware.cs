using System.Diagnostics;

namespace ProductsDomain.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;

        // Log incoming request
        logger.LogInformation(
            "→ Incoming Request: {Method} {Path}{QueryString}",
            request.Method,
            request.Path,
            request.QueryString);


        await next(context);

        stopwatch.Stop();
        logger.LogInformation(
            "← Response: {StatusCode} for {Method} {Path} - {ElapsedMs}ms",
            context.Response.StatusCode,
            request.Method,
            request.Path,
            stopwatch.ElapsedMilliseconds);
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
