using DotBelt.CMS.Shared.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Path = System.IO.Path;

namespace DotBelt.CMS.Shared.CMS.Media;

public static class UploadsApiEndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MapUploadsApi(this IEndpointRouteBuilder endpoints)
    {
        var routeGroup = endpoints.MapGroup("");

        routeGroup.MapPost("", async Task<Results<Ok<List<Upload>>, ValidationProblem>>
            (HttpRequest request, HttpContext context, [FromServices] IServiceProvider sp) =>
        {
            using var scope = sp.CreateScope();
            
            var uploadsService = scope.ServiceProvider.GetService<UploadsController>();

            var userId = context.User.GetUserId();

            if (uploadsService == null)
            {
                throw new InvalidOperationException("Could not find uploads service.");
                
            }
            
            var files = context.Request.Form.Files;
          
            var uploads = await uploadsService.CreateUploadsAsync(files, userId);

            return TypedResults.Ok(uploads);
        }).DisableAntiforgery();

      
 

        return routeGroup;
       

    }
}