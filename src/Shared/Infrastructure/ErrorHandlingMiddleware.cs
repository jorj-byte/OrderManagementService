using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Identity;

namespace Shared.Infrastructure;

// Shared/Infrastructure/ErrorHandling/ErrorHandlingMiddleware.cs
public class ErrorHandlingMiddleware:IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware( ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }
  
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access");
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync( "Forbidden" );
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Entity not found");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("Not Found" );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync( "Internal Server Error" );
        }

    }
}
public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
