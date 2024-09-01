using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotBelt.CMS.Shared.ManualMigrations;

[RegisterScoped]
public class ManualMigrator
{
    private readonly ApplicationDbContext _dbContext;
    private readonly HttpClient _httpClient;
    private readonly ILogger<ManualMigrator> _logger;

    private readonly List<ManualMigrationItem> _migrationConfs = new();

    public ManualMigrator(IDbContextFactory<ApplicationDbContext> dbContextFactory,
        ILogger<ManualMigrator> logger)
    {
        _logger = logger;
        _dbContext = dbContextFactory.CreateDbContext();

        SetupMigrations();
    }

    private void SetupMigrations()
    {
    }

    public async Task DoInitialMigration()
    {
        foreach (var migration in _migrationConfs)
        {
            await migration.ApplyMigration();
        }

        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        _httpClient.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        if (_httpClient is IAsyncDisposable httpClientAsyncDisposable)
            await httpClientAsyncDisposable.DisposeAsync();
        else
            _httpClient.Dispose();
    }
}