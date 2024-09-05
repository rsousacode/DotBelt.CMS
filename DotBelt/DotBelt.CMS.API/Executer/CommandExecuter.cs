using BoilerPlateSSR.Internal;

namespace BoilerPlateSSR.Swagger;

public class CommandExecuter
{
    public List<Command> Commands { get; set; } = new();

    private readonly string[]? _args;
   

    public CommandExecuter(string[] args)
    {
        if (args.Length == 0) return;
        
        _args = args;
        
        var types = Reflection.GetTypesUsingAttribute<CommandLineArgAttribute>();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type) as Command;
            
            if(instance == null) 
            {
                throw new NullReferenceException($"Instance of type {type.FullName} could not be instantiated.");
            }
            
            Commands.Add(instance);
        }
    }
    
    public Command? GetCommandToExecute()
    {
        if (_args?.Length == 0) return null;

        var firstArgument = _args![0];

        var commandToExecute = Commands.FirstOrDefault(x => x.FirstArg == firstArgument);

        if (commandToExecute == null) return null;

        return commandToExecute;
    }
}

public static class CommandExecuterExtensions 
{
    public static async Task<int> RunWithCommandExecuter(
        this IHost host,
        string[] args)
    {
        var commmandToExecute = new CommandExecuter(args).GetCommandToExecute();
        
        if (commmandToExecute != null)
        {
            host.Start();
            commmandToExecute.Execute(args);
        }
        else
        {
            await host.RunAsync();
        }

        return 0;
    }

}