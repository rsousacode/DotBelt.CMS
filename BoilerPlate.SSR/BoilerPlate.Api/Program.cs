using System.Text.Json.Serialization;
using BoilerPlate.Shared;
using BoilerPlate.Shared.ManualMigrations;
using BoilerPlate.Shared.Users;
using BoilerPlateSSR.Endpoints;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateSlimBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddAuthentication();
services.AddAuthorization();

services.AutoRegisterFromBoilerPlateShared();

var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

var dataSource = dataSourceBuilder.Build();
services
    .AddPooledDbContextFactory<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(dataSource,
            npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("BoilerPlate.Shared");
            });

        options.UseModel(MyCompiledModels.ApplicationDbContextModel.Instance);
    });

services.AddScoped<ApplicationDbContext>(serviceProvider =>
{
    var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
    return dbContextFactory.CreateDbContext();
});


services
    .AddIdentityApiEndpoints<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

services.AddEndpointsApiExplorer();


builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});


var app = builder.Build();


app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseStaticFiles("/static");

app.UseAuthentication();

app.UseAuthorization();



app.MapGroup("/auth")
    .MapIdentityApi<ApplicationUser>();

app.RegisterRootEndpointHandler();

await using (var scope = app.Services.CreateAsyncScope())
{
    var migrator = scope.ServiceProvider.GetService<ManualMigrator>();
    await migrator!.DoInitialMigration();
}


app.Run();


[JsonSerializable(typeof(UserResponse[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}