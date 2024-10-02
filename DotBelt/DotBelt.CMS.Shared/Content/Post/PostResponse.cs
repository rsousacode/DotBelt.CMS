using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.CMS.Media;
using DotBelt.CMS.Shared.Content.Taxonomies;
using DotBelt.CMS.Shared.Users;

namespace DotBelt.CMS.Shared.Content.Post;

public class PostResponse
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public required string? Description { get; set; }
    public required string RelativeUrl { get; set; }
    
    public string? FullUrl { get; set; }
    
    public string? Content { get; set; }
    
    public DateTimeOffset? PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    
    public UserResponse? Author { get; set; }
    public int? AuthorId { get; set; }
    public PostTypeEnum? PostType { get; set; }
    
    public int? ParentPostId { get; set; }
    
    public UploadResponse? FeaturedImage { get; set; }
    
    public int? FeaturedImageId { get; set; }
    public PostStatus Status { get; set; }

    public IEnumerable<TaxonomyResponse>? Taxonomies { get; set; } = null!;
}