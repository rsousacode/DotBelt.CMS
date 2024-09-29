using System.CommandLine;
using BoilerPlateSSR.Swagger;
using DotBelt.CMS.Shared.CMS.Media;
using DotBelt.CMS.Shared.Tenants;
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
            throw new InvalidOperationException("Could not fi§nd ApplicationDbContext");
        }

        var tenant = new Tenant()
        {
            AllowedFileTypes = Mimes.Images,
            Name = "boilerplate.com",
            FullUrl = "https://boilerplate.com"
        };

        dbContext.Add(tenant);
        
        await dbContext.SaveChangesAsync();

        var user = await SeedAdmin.GetCreatedAdminUser(scope.ServiceProvider);
        
        var post = SeedPages.GetHelloWorld(user.Id, tenant.Id);     
        
        dbContext.Add(post);
        
        await dbContext.SaveChangesAsync();
    }
}