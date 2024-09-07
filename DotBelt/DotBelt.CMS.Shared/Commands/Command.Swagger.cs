using System.CommandLine;
using System.CommandLine.Binding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace BoilerPlateSSR.Swagger;

[CommandLineArg("swagger")]
public class CommandSwagger : System.CommandLine.Command
{
    public CommandSwagger() : base("swagger")
    {
        this.Description = "A command line tool for a GraphQL Server.";
        
        this.SetHandler<IHost>( CommandSwagger.ExecuteAsync,  (IValueDescriptor<IHost>) Bind.FromServiceProvider<IHost>());

    }
    
    private static void ExecuteAsync(
        IHost host)
    {

        ISwaggerProvider? swaggerProvider = host.Services.GetService<ISwaggerProvider>();
        if (swaggerProvider == null)
        {
            throw new InvalidOperationException($"No service {nameof(ISwaggerProvider)} found.");
        }

        OpenApiDocument document = swaggerProvider.GetSwagger("v1");
        
        Console.WriteLine(document.SerializeAsJson(OpenApiSpecVersion.OpenApi3_0));

    }

}