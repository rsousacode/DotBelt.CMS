using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.QueriesMutations;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace DotBelt.Queries;

[ExtendObjectType(typeof(GraphQLQuery))]
public class TaxonomiesQueries
{
    // TODO: Use DTOs
    
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<Taxonomy> GetTaxonomies([Service(ServiceKind.Pooled)] ApplicationDbContext context)
    {
        return context
            .Taxonomies;
    }
    
    // TODO: Use DTOs
    
    [UseProjection]

    public IQueryable<Taxonomy> GetTaxonomyById(int id,  ApplicationDbContext context)
    {
        return context
            .Taxonomies
            .Where(x => x.Id == id);
    }
    
    
}