namespace BoilerPlateSSR.Swagger;

[CommandLineArg("swagger")]
public class SwaggerCommands : Command
{
    public override void Execute(string[] args)
    {
        Console.WriteLine("Hello World!");
    }
}