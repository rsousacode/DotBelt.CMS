using BoilerPlate.Shared.CMS;
using BoilerPlate.Shared.ManualMigrations;
using BoilerPlate.Shared.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Shared;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("uuid-ossp");        

    }
    
    public DbSet<ManualMigration> ManualMigrations { get; set; }
    
    public DbSet<Post> Posts { get; set; } 
    public DbSet<Taxonomy> Taxonomies { get; set; }
    

    
}