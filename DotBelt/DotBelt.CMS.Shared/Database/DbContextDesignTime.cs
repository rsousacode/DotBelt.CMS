using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace DotBelt.CMS.Shared;

public class DbContextDesignTime : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        var dataSourceBuilder = new NpgsqlDataSourceBuilder("Server=localhost;Port=5435;Database=Boilerplate;User Id=postgres;Password=postgres;");
        //dataSourceBuilder.UseNetTopologySuite();
        var dataSource = dataSourceBuilder.Build();
        //optionsBuilder.UseNpgsql(dataSource, options => options.UseNetTopologySuite());
        optionsBuilder.UseNpgsql(dataSource);
        return new ApplicationDbContext(optionsBuilder.Options);
    }
  
}