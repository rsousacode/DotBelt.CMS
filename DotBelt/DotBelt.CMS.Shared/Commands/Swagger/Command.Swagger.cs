using System.CommandLine;
using System.CommandLine.Binding;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Swagger;
using ILogger = Serilog.ILogger;

namespace BoilerPlateSSR.Swagger;
internal static class Opt<TOption> where TOption : new()
{
    private static TOption? _instance;

    /// <summary>
    /// Gets the singleton instance of <typeparamref name="TOption" />
    /// </summary>
    public static TOption Instance => _instance ??= new TOption();
}
internal class OutputOption : Option<FileInfo?>
{
    public OutputOption()
        : base("--output")
    {
        Description = "The path to the file where the schema should be exported to";
    }
}

public sealed class CommandSwagger : Command
{
    public CommandSwagger() : base("swagger")
    {
        Description = "Export the swagger.json. If no output (--output) is specified the schema will be printed to the console.";
        AddOption((Option) Opt<OutputOption>.Instance);
        this.SetHandler
            ( ExecuteAsync,  
                Bind.FromServiceProvider<IHost>(), 
                (IValueDescriptor<FileInfo>) Opt<OutputOption>.Instance);

    }
    
    private static async Task ExecuteAsync(
        IHost host, FileInfo file) 
    {

        host.Start();
        var schema = await SwaggerExtensions.GetJsonSchema(host.Services);
        if (schema != null)
            await File.WriteAllTextAsync(file.FullName, schema, Encoding.UTF8);
        else
            Console.WriteLine(schema);
    }

}