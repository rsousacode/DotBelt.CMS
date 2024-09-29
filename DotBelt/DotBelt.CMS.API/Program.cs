using System;
using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.ManualMigrations;
using DotBelt.CMS.Shared.Users;
using BoilerPlateSSR.Swagger;
using DotBelt.CMS.Shared.CMS.Media;
using DotBelt.CMS.Shared.Identity;
using DotBelt.CMS.Shared.Internal;
using DotBelt.Identity;
using DotBelt.QueriesMutations;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    if (args.Length is not 0 )
    {
        builder.WebHost.UseUrls("http://0.0.0.0:8656");
    }
    
    var configuration = builder.Configuration;

    var services = builder.Services;
    

    services.AddAuthentication();
   
    services.AddAuthorization();
    
    services.AddAntiforgery(options =>
    {
        options.HeaderName = "X-XSRF-TOKEN"; 
    });

    services.AutoRegisterFromDotBeltCMSShared();

    var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                           throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

    var dataSource = dataSourceBuilder.Build();
    services
        .AddPooledDbContextFactory<ApplicationDbContext>(options =>
        {
            options
                .UseNpgsql(dataSource,
                npgsqlOptions => 
                {
                    npgsqlOptions.MigrationsAssembly("DotBelt.CMS.Shared");
                    
                })
                .EnableSensitiveDataLogging();
        });

    services.AddScoped<ApplicationDbContext>(serviceProvider =>
    {
        var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
        return dbContextFactory.CreateDbContext();
    });

    services.ConfigureGraphQl();

    services
        .AddIdentityApiEndpoints<ApplicationUser>(options => { options.SignIn.RequireConfirmedAccount = true; })
        .AddEntityFrameworkStores<ApplicationDbContext>();
  
    services.AddEndpointsApiExplorer();
    
    services.AddSwaggerGen();
    
    services.AddTransient<IEmailSender, EmailSender>();
    
    services.AddTransient<IEmailSender<ApplicationUser>, IdentityEmailSender>();
    
    services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
    
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
    app.UseAntiforgery();

    app.MapGraphQL("/api/graphql").WithOptions(new GraphQLServerOptions()
    {
        EnableSchemaRequests = builder.Environment.IsDevelopment(),
        AllowedGetOperations = AllowedGetOperations.Query,
        Tool =
        {
            Enable = builder.Environment.IsDevelopment(),
        }
    });

    app.MapBananaCakePop("/api/banana", relativeRequestPath: "/api/graphql");

    app.MapGroup("/api/auth")
        .MapIdentityApi<ApplicationUser>();

    app.MapGroup("/api/uploads")
        .MapUploadsApi();
    
    app.UseStaticFiles();

    await using (var scope = app.Services.CreateAsyncScope())
    {
        var migrator = scope.ServiceProvider.GetService<ManualMigrator>();
        await migrator!.DoInitialMigration();
    }
        
    await app.RunWithCommandExecuter(args);

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
