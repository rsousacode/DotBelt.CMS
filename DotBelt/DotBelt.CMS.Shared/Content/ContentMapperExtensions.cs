using DotBelt.CMS.Shared.CMS.Media;
using DotBelt.CMS.Shared.Content.Post;
using DotBelt.CMS.Shared.Content.Taxonomies;
using Riok.Mapperly.Abstractions;

namespace DotBelt.CMS.Shared.CMS;

[Mapper]
public static partial class ContentMapperExtensions
{
    public static partial IQueryable<PostResponse> ProjectToPostResponse(this IQueryable<Post> q);
    public static partial IQueryable<TaxonomyResponse> ProjectToTaxonomyResponse(this IQueryable<Taxonomy> q);
   
    public static partial IQueryable<UploadResponse> ProjectToUploadResponse(this IQueryable<Upload> q);
   
    
}