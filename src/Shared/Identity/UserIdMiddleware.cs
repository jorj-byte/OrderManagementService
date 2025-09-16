using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Shared.Identity;

public class UserIdMiddleware:IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.Request.Headers.TryGetValue("X-User-Id", out var userIdHeader) ||
            !Guid.TryParse(userIdHeader, out var userId))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("X-User-Id header is required");
            return;
        }

        // Store in HttpContext for later use (controllers, services)
        context.Items["UserId"] = userId;

        await next(context);
    }
}

// Extension method
public static class UserIdMiddlewareExtensions
{
    public static IApplicationBuilder UseUserIdMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserIdMiddleware>();
    }
}