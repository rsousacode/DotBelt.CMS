using System.Text.Json.Serialization;
using BoilerPlate.Shared;
using BoilerPlate.Shared.ManualMigrations;
using BoilerPlate.Shared.Users;
using BoilerPlateSSR.Endpoints;
using BoilerPlateSSR.Queries;
using BoilerPlateSSR.QueriesMutations;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

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

        //options.UseModel(MyCompiledModels.ApplicationDbContextModel.Instance);
    });

services.AddScoped<ApplicationDbContext>(serviceProvider =>
{
    var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
    return dbContextFactory.CreateDbContext();
});

services
    .AddGraphQLServer()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddMutationConventions()
    .AddQueryType<GraphQLQuery>()
    .AddTypeExtension<PostsQueries>()
    .AddTypeExtension<TaxonomiesQueries>()
    .AddMutationType<GraphQLMutation>()
    .AddTypeExtension<CreatePostMutation>()
    .AddTypeExtension<EditPostMutation>()
    .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled);

services
    .AddIdentityApiEndpoints<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
services.AddEndpointsApiExplorer();

#if DEBUG
services.AddSwaggerGen();
#endif
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

app.UseAuthorization();

app.MapGraphQL("/api/graphql").WithOptions(new GraphQLServerOptions()
{
    EnableSchemaRequests = builder.Environment.IsDevelopment(),
    AllowedGetOperations = AllowedGetOperations.Query,
    Tool =
    {
        Enable = builder.Environment.IsDevelopment(),
    }
});

app.MapBananaCakePop("/api/banana", relativeRequestPath:"/api/graphql");

app.MapGroup("/api/auth")
    .MapIdentityApi<ApplicationUser>();

app.RegisterRootEndpointHandler();

await using (var scope = app.Services.CreateAsyncScope())
{
    var migrator = scope.ServiceProvider.GetService<ManualMigrator>();
    await migrator!.DoInitialMigration();
}


await app.RunWithGraphQLCommandsAsync(args);


//app.Run();


[JsonSerializable(typeof(UserResponse[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}