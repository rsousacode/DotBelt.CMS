using BoilerPlate.Shared.Users;

namespace BoilerPlate.Shared.CMS;

public class Taxonomy : IContent
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string UrlName { get; set; }
    public DateTimeOffset PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }

    public string Description { get; set; }
    public ApplicationUser? Author { get; set; }
    public string? AuthorId { get; set; }

    public TaxonomyTypeEnum Type { get; set; }
    
    public Taxonomy? ParentTaxonomy { get; set; }
    public Taxonomy[] ChildrenTaxonomies { get; set; }
    
    
    public Post[] Posts { get; set; }
}