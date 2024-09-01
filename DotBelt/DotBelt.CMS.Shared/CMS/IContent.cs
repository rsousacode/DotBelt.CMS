using DotBelt.CMS.Shared.Users;

namespace DotBelt.CMS.Shared.CMS;

public interface IContent
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ApplicationUser? Author { get; set; }
    public string? AuthorId { get; set; }
    
    public string UrlName { get; set; }

    
        
    public DateTimeOffset PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
}