using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DotBelt.CMS.Shared.Identity;

public static class IdentityExtensions
{
    public static int? GetUserId(this ClaimsPrincipal principal)
    {
        var id = principal
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;
        
        if(id != null && int.TryParse(id, out int userId))
        {
            return userId;
        }

        return null;

    }
    
    public static int? GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;

        var user = httpContext?.User;
        
        return user?.GetUserId();
    }
    
    
}