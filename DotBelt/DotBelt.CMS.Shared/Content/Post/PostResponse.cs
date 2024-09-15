using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.Content.Taxonomies;
using DotBelt.CMS.Shared.Users;

namespace DotBelt.CMS.Shared.Content.Post;

public class PostResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public required string? Description { get; set; }
    public required string UrlName { get; set; }
    
    public string? FullUrl { get; set; }
    
    public string? Content { get; set; }
    public string? ContentHtml { get; set; }
    
    public DateTimeOffset PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    
    public UserResponse? Author { get; set; }
    public int? AuthorId { get; set; }
    public PostTypeEnum PostType { get; set; }
    
    public int? ParentPostId { get; set; }
    
    public TaxonomyResponse[] Taxonomies { get; set; } = null!;
}