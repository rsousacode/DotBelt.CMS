using BoilerPlateSSR.QueriesMutations;
using DotBelt.Mutations.Posts;
using DotBelt.Mutations.Posts.Create;
using DotBelt.Queries;
using DotBelt.CMS.Shared;
using HotChocolate.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DotBelt.QueriesMutations;

public static class GraphQLConfigurator
{
    public static IServiceCollection ConfigureGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddDiagnosticEventListener<ErrorLoggingDiagnosticsEventListener>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddMutationConventions()
            .AddQueryType<GraphQLQuery>()
            .AddTypeExtension<PostsQueries>()
            .AddTypeExtension<TaxonomiesQueries>()
            .AddMutationType<GraphQLMutation>()
            .AddTypeExtension<Create>()
            .AddTypeExtension<Edit>()
            .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled);

        return services;
    }
}