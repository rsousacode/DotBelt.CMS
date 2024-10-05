using Riok.Mapperly.Abstractions;

namespace DotBelt.CMS.Shared.Tenants;

[Mapper]
public static partial class TenantMapperExtensions
{
    public static partial IQueryable<TenantResponse> ProjectToTenantResponse(this IQueryable<Tenant> q);
    public static partial TenantResponse ToTenantResponse(this Tenant t);
    
}