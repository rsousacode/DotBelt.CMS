namespace DotBelt.CMS.Shared.Tenants;

public class TenantResponse
{
    public int? Id { get; set; }

    public required string Name { get; set; }
    
    public required string FullUrl { get; set; }

    public required string[] AllowedFileTypes { get; set; }
    
    public int? HomepageId { get; set; }

}