using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.Content.Post;
using DotBelt.CMS.Shared.Users;

namespace DotBelt.CMS.Shared.Content.Taxonomies;

public class TaxonomyResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public required string UrlName { get; set; }
    public DateTimeOffset PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }

    public required string? Description { get; set; }
    public UserResponse? Author { get; set; }
    public int? AuthorId { get; set; }

    public TaxonomyTypeEnum Type { get; set; }
    
    public int? ParentTaxonomyId { get; set; }
}