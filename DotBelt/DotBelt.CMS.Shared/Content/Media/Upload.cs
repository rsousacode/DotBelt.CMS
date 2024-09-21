using DotBelt.CMS.Shared.Tenants;
using DotBelt.CMS.Shared.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotBelt.CMS.Shared.CMS.Media;

public class UploadConfiguration : IEntityTypeConfiguration<Upload> 
{
    public void Configure(EntityTypeBuilder<Upload> builder)
    {
        builder
            .Property(x => x.FullUrl)
            .HasMaxLength(255);
        
        builder
            .Property(x => x.RelativeUrl)
            .HasMaxLength(255);
   
        builder
            .Property(x => x.CropName)
            .HasMaxLength(90);

        builder.HasIndex(x => x.CropName);

        builder
            .Property(x => x.MetaData)
            .HasColumnType("jsonb");
    }
}

public class Upload : IContent
{
    public int Id { get; set; }
    public string? Title { get; set; }
    
    public string? CropName { get; set; }
    public string? Description { get; set; }
    public required string FileName { get; set; }
    public required string MimeType { get; set; }
    public int Length { get; set; }
    public required ApplicationUser Author { get; set; }
    public int AuthorId { get; set; }
    public required string RelativeUrl { get; set; }
    public string? FullUrl { get; set; }
    public bool? InTrash { get; set; }
    public DateTimeOffset PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public required Tenant Tenant { get; set; }
    public int TenantId { get; set; }

    public string? MetaData { get; set; }

    public ICollection<Upload> Children { get; set; } = null!;
      
    public Upload? Parent { get; set; }
    public int? ParentId { get; set; }
     
}