using System.Reflection;
using BoilerPlateSSR.Internal;

namespace BoilerPlateSSR.Swagger;

public abstract class Command
{
    public string FirstArg;
    
    public Command()
    {
        var type = GetType();
        
        var attribute = type.GetCustomAttribute(typeof(CommandLineArgAttribute));
        
        if (attribute is not CommandLineArgAttribute commandLineArgAttribute) 
        {
            throw new InvalidOperationException($" {type.Name} has to have a CommandLineArgAttribute");
        }

        FirstArg = commandLineArgAttribute.CommandName;

    }
    
    public void TryExecute(string[] args) 
    {
        if(args.Length > 0) 
        {
            if(args[0] == FirstArg) 
            {
                Execute(args);
            }
        }
    }

    public abstract void Execute(string[] args);
}
