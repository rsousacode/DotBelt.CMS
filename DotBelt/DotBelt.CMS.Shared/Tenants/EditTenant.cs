using DotBelt.QueriesMutations;
using Microsoft.EntityFrameworkCore;

namespace DotBelt.CMS.Shared.Tenants;

[ExtendObjectType<DotBeltMutation>]
public class EditTenant
{
    public async Task<TenantResponse?> EditTenantAsync(TenantResponse tenant, ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        var tenantDb = await context
            .Tenants
            .FirstOrDefaultAsync(x => x.Id == tenant.Id, cancellationToken: cancellationToken);

        if (tenantDb == null)
        {
            return null;
        }
        
        tenantDb.HomepageId = tenant.HomepageId;
        tenantDb.Name = tenant.Name;
        tenantDb.FullUrl = tenant.FullUrl;
        tenantDb.AllowedFileTypes = tenant.AllowedFileTypes;

        await context.SaveChangesAsync(cancellationToken);

        return tenantDb.ToTenantResponse();
    }
}