using System.CommandLine;
using DotBelt.CMS.Shared.Commands.Seeding;

namespace BoilerPlateSSR.Swagger;

public class RootCommand : Command
{
    public RootCommand() : base("dotbelt")
    {
        Description = "utility commands";
        AddCommand(new CommandSwagger());
        AddCommand(new Command_Seeding());
    }
}