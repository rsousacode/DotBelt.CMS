using DotBelt.CMS.Shared.CMS.Media;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotBelt.CMS.Shared.Identity;

public static class AntiForgeryEndpointRouteBuilderExtensions
{
    /*public static IEndpointConventionBuilder MapAntiForgeryTokenApi(this IEndpointRouteBuilder endpoints)
    {
        var routeGroup = endpoints.MapGroup("");

        routeGroup.MapPost("", async Task<Results<Ok<List<Upload>>, ValidationProblem>>
            ([FromForm] UploadRequest upload, HttpContext context, [FromServices] IServiceProvider sp) =>
        {
            using var scope = sp.CreateScope();
            
            var antiforgery = scope.ServiceProvider.GetService<IAntiforgery>();

            var token = antiforgery!.GetTokens(context);
            
            return TypedResults.Ok(token.);
        });

        return routeGroup;
       

    }*/
}