using DotBelt.CMS.Shared.Users;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlateSSR.Endpoints;

public static class RootEndpoint
{
    public static void RegisterRootEndpointHandler(this WebApplication app)
    {
        app.MapGroup("/api")
            .MapGet("/users", async ([FromServices] UsersService usersService) => 
        {
            var users = await usersService.GetUsers();
            return users;
        });

    }
}