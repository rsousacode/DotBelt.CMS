using DotBelt.CMS.Shared.Users;

namespace DotBelt.CMS.Shared.CMS;

public class Taxonomy : IContent
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public required string UrlName { get; set; }
    public DateTimeOffset PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }

    public required string? Description { get; set; }
    public ApplicationUser? Author { get; set; }
    public string? AuthorId { get; set; }

    public TaxonomyTypeEnum Type { get; set; }
    
    public Taxonomy? ParentTaxonomy { get; set; }
    public Taxonomy[] ChildrenTaxonomies { get; set; } = null!;


    public Post[] Posts { get; set; } = null!;
}