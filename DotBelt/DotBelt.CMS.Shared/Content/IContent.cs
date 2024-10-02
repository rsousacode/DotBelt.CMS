using DotBelt.CMS.Shared.Tenants;
using DotBelt.CMS.Shared.Users;

namespace DotBelt.CMS.Shared.CMS;

public interface IContent
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ApplicationUser? Author { get; set; }
    public int AuthorId { get; set; }
    
    public string RelativeUrl { get; set; }
    
    public string? FullUrl { get; set; }
    
    public bool? InTrash { get; set; }

    
        
    public DateTimeOffset PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    
    public Tenant? Tenant { get; set; }
    
    public int TenantId { get; set; }
}