using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Shared.ManualMigrations;

public class ManualMigrationItem
{
    private string Name { get; set; }
    protected readonly ApplicationDbContext _dbContext;
    protected bool _autoCompleteMigration = true;

    protected ManualMigrationItem(string name, ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Name = name;
    }

    private async Task<bool> CanMigrate()
    {
        return await _dbContext.ManualMigrations.Where(x => x.Name == Name).AnyAsync() == false;
    }

    protected virtual Task RunMigration()
    {
        return Task.CompletedTask;
    }

    protected void AssumeMigrationComplete()
    {
        _dbContext.ManualMigrations.Add(new ManualMigration()
        {
            Name = Name
        });
    }

    public async Task ApplyMigration()
    {
        if (!await CanMigrate()) return;
        await RunMigration();
        if (_autoCompleteMigration)
        {
            AssumeMigrationComplete();
        }

        await _dbContext.SaveChangesAsync();

    }
}