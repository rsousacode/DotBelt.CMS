using DotBelt.CMS.Shared.CMS;

namespace DotBelt.CMS.Shared.Tenants;

public class Tenant
{
    public int Id { get; set; }

    public required string Name { get; set; }
    
    public required string FullUrl { get; set; }

    public required string[] AllowedFileTypes { get; set; }

    public ICollection<Post> Posts { get; set; } = null!;

    public ICollection<Taxonomy> Taxonomies { get; set; } = null!;
}