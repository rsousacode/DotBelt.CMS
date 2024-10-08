using System.Text.Json;
using BoilerPlateSSR.QueriesMutations;
using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.GraphQL;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotBelt.QueriesMutations
{
    public static class GraphQLConfigurator
    {
        private static IRequestExecutorBuilder AddGraphQlTypeExtensions(this IRequestExecutorBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var typesWithAttribute = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttributes(true)
                        .Any(IsExtendObjectTypeAttribute))
                .ToList();

            foreach (var type in typesWithAttribute)
            {
                builder.AddTypeExtension(type);
            }

            return builder;
        }

        private static bool IsExtendObjectTypeAttribute(object attribute)
        {
            var attributeType = attribute.GetType();
            if (attributeType.IsGenericType && attributeType.GetGenericTypeDefinition() == typeof(ExtendObjectTypeAttribute<>))
            {
                return true;
            }

            if (attributeType == typeof(ExtendObjectTypeAttribute))
            {
                return true;
            }

            return false;
        }

        public static IServiceCollection ConfigureGraphQl(this IServiceCollection services)
        {        
            services
                .AddGraphQLServer()
                .AddAuthorization()
                .AddDiagnosticEventListener<ErrorLoggingDiagnosticsEventListener>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddMutationConventions()
                .AddQueryType<DotBeltQuery>()
                .AddMutationType<DotBeltMutation>()
                .BindRuntimeType<IDictionary<string, object>, AnyType>()
                .AddGraphQlTypeExtensions()
                .AddApolloTracing()
                .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled);

            services.AddScoped<GraphQlExecutor>();

            return services;
        }
    }
}