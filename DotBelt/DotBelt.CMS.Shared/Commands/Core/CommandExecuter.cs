using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Hosting;
namespace BoilerPlateSSR.Swagger;

#nullable disable

public class CommandExecuter : CommandLineBuilder
{
    public CommandExecuter(IHost host)
        : base(new RootCommand())
    {
        this.AddMiddleware((Action<InvocationContext>) (x => x.BindingContext.AddService<IHost>((Func<IServiceProvider, IHost>) (_ => host)))).UseDefaults();
    }
    
}

public static class CommandExecuterExtensions 
{
    private static bool IsValidCommand(this string[] args)
    {
        
        return args != null && args.Length >= 1 && args[0] == "dotbelt";
    }
    public static async Task<int> RunWithCommandExecuter(
        this IHost host,
        string[] args)
    {
        if (IsValidCommand(args))
        {
            await new CommandExecuter(host).Build().InvokeAsync(args);
        }
        else 
        {
            await host.RunWithGraphQLCommandsAsync(args);
        }

        return 0;
    }

}