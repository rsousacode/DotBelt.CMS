namespace BoilerPlateSSR.Swagger;

public class CommandLineArgAttribute(string commandName) : Attribute
{
    public string CommandName { get; set; } = commandName;
}