using DotBelt.CMS.Shared.CMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotBelt.CMS.Shared.Tenants;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant> 
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Tenant);
        
        builder.HasOne(x => x.Homepage)
            .WithOne()
            .HasForeignKey<Tenant>(x => x.HomepageId);
    }
}
public class Tenant
{
    public int Id { get; set; }

    public required string Name { get; set; }
    
    public required string FullUrl { get; set; }
    
    public int? HomepageId { get; set; }
    
    public Post? Homepage { get; set; }

    public required string[] AllowedFileTypes { get; set; }

    public ICollection<Post> Posts { get; set; } = null!;

    public ICollection<Taxonomy> Taxonomies { get; set; } = null!;
}