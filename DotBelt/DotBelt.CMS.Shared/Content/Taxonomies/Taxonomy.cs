using DotBelt.CMS.Shared.Tenants;
using DotBelt.CMS.Shared.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotBelt.CMS.Shared.CMS;

public class TaxonomyConfiguration : IEntityTypeConfiguration<Taxonomy> 
{
    public void Configure(EntityTypeBuilder<Taxonomy> builder)
    {
        builder
            .Property(x => x.FullUrl)
            .HasMaxLength(255);
        
        builder
            .Property(x => x.RelativeUrl)
            .HasMaxLength(255);

    }
}

public class Taxonomy : IContent
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public required string RelativeUrl { get; set; }
    
    public string? FullUrl { get; set; }
    
    public bool? InTrash { get; set; }
    public DateTimeOffset PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    
    public Tenant? Tenant { get; set; }
    public int TenantId { get; set; }

    public required string? Description { get; set; }
    public ApplicationUser? Author { get; set; }
    public required int AuthorId { get; set; }

    public TaxonomyTypeEnum Type { get; set; }
    
    public int? ParentTaxonomyId { get; set; }


    public ICollection<Post> Posts { get; set; } = null!;
}