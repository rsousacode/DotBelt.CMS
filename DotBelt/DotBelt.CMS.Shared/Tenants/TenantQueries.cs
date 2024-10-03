using DotBelt.QueriesMutations;

namespace DotBelt.CMS.Shared.Tenants;

[ExtendObjectType<DotBeltQuery>]
public class TenantQueries
{
    public IQueryable<TenantResponse> GetTenantById(int id, ApplicationDbContext context)
    {
        return context
            .Tenants
            .Where(x => x.Id == id)
            .ProjectToTenantResponse();
    }
}