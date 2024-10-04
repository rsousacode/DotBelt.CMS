using System.Reflection;
using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.CMS.Media;
using DotBelt.CMS.Shared.ManualMigrations;
using DotBelt.CMS.Shared.Tenants;
using DotBelt.CMS.Shared.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotBelt.CMS.Shared;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfiguration(new PostTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TaxonomyConfiguration());
        modelBuilder.ApplyConfiguration(new UploadConfiguration());
        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        
        modelBuilder.HasPostgresExtension("uuid-ossp");        
        
    }
    
    public DbSet<ManualMigration> ManualMigrations { get; set; }
    
    public DbSet<Post> Posts { get; set; } 
    public DbSet<Taxonomy> Taxonomies { get; set; }
    
    public DbSet<Upload> Uploads { get; set; }
     
    public DbSet<Thumbnail> Thumbnails { get; set; }
    
    public DbSet<Tenant> Tenants { get; set; }
    

    
}