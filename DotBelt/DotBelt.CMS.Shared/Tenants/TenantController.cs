using Microsoft.EntityFrameworkCore;

namespace DotBelt.CMS.Shared.Tenants;

[RegisterScoped]
public class TenantController
{
    private readonly ApplicationDbContext _context;
    
    public TenantController(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _context = contextFactory.CreateDbContext();
    }
    
    public async Task <string[]?> GetAllowedFileTypes(CancellationToken cancellationToken = default)
    {
        return await _context
            .Tenants
            .Select(x => x.AllowedFileTypes)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
    
    public async Task <string[]?> GetAllowedFileTypes(int tenantId, CancellationToken cancellationToken = default)
    {
        return await _context
            .Tenants
            .Where(x => x.Id == tenantId)
            .Select(x => x.AllowedFileTypes)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
    
    
}