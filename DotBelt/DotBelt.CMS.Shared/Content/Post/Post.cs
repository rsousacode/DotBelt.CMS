using System.ComponentModel.DataAnnotations.Schema;
using DotBelt.CMS.Shared.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotBelt.CMS.Shared.CMS;

public class PostTypeConfiguration : IEntityTypeConfiguration<Post> 
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .Property(x => x.Content)
            .HasColumnType("jsonb");
        
        builder
            .Property(x => x.PostType)
            .HasConversion<string>();

        builder
            .HasIndex(x => x.FullUrl)
            .IsUnique();
    }
}

public class Post : IContent
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
    
    public ApplicationUser? Author { get; set; }
    public int? AuthorId { get; set; }
    public PostTypeEnum PostType { get; set; }
    
    public Post? ParentPost { get; set; }
    public int? ParentPostId { get; set; }
    public Post[] ChildrenPosts { get; set; } = null!;
    
    
    public Taxonomy[] Taxonomies { get; set; } = null!;
}