using Microsoft.AspNetCore.Mvc;

namespace Shared.Identity;

public static class ControllerExtensions
{
    public static Guid GetUserId(this ControllerBase controller)
    {
        if (controller.HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is Guid userId)
        {
            return userId;
        }
        
        throw new UnauthorizedAccessException("User ID not found in the request context.");
    }
}