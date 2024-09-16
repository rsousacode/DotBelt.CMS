using System.Security.Claims;
using DotBelt.QueriesMutations;
using Microsoft.AspNetCore.Http;

namespace DotBelt.CMS.Shared.Identity;

public class SessionData
{
    public int? Id { get; set; }
    public string? Email { get; set; } = "";
    
    public string? UserName { get; set; } = "";
    
    public bool IsAuthenticated { get; set; }

}

[ExtendObjectType<GraphQLQuery>]
public class Session
{
    public SessionData GetSession([Service] IHttpContextAccessor httpContextAccessor)
    {
        
        if(httpContextAccessor.HttpContext == null) 
        {
            throw new InvalidOperationException("HttpContext is null");
        }
        
        var user = httpContextAccessor.HttpContext.User;
        
        if(user.Identity == null) 
        {
            return new SessionData();
        }

        return new SessionData()
        {
            IsAuthenticated = user.Identity.IsAuthenticated,
            Id = user
                .Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault(),
            Email = user
                .Claims
                .Where(c => c.Type == ClaimTypes.Email)
                .Select(x => x.Value)
                .FirstOrDefault(),
            UserName = user
                .Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(x => x.Value)
                .FirstOrDefault(),
            
        };

    }
}