using System.Reflection;
using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.ManualMigrations;
using DotBelt.CMS.Shared.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotBelt.CMS.Shared;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfiguration(new PostTypeConfiguration());
        modelBuilder.HasPostgresExtension("uuid-ossp");        
        
    }
    
    public DbSet<ManualMigration> ManualMigrations { get; set; }
    
    public DbSet<Post> Posts { get; set; } 
    public DbSet<Taxonomy> Taxonomies { get; set; }
    

    
}