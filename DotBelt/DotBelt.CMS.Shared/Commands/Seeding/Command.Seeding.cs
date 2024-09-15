using System.CommandLine;
using BoilerPlateSSR.Swagger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotBelt.CMS.Shared.Commands.Seeding;

public class Command_Seeding : Command
{
    public Command_Seeding() : base("seed")
    {
        Description = "Seed data into the application";
        this.SetHandler
        ( ExecuteAsync,  
            Bind.FromServiceProvider<IHost>());

    }
    
    private static async Task ExecuteAsync(
        IHost host) 
    {
        host.Start();
        
        using var scope = host.Services.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        
        if(dbContext == null) 
        {
            throw new InvalidOperationException("Could not find ApplicationDbContext");
        }

        var user = SeedAdmin.GetAdminUser();
        
        dbContext.Users.Add(user);
       
        var post = SeedPages.GetHelloWorld();       

        dbContext.Add(post);
        
        await dbContext.SaveChangesAsync();
    }
}