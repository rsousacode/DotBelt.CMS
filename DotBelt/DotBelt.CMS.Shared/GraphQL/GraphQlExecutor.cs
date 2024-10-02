using HotChocolate.Execution;

namespace DotBelt.CMS.Shared.GraphQL;

public class GraphQlQuery 
{
    public required string Query { get; set; }
    
    public Dictionary<string, object?> Variables { get; set; } = new Dictionary<string, object?>();
    
}

public class GraphQlExecutor
{
    private readonly IRequestExecutorResolver _requestResolver;

    public GraphQlExecutor(IRequestExecutorResolver requestResolver)
    {
        _requestResolver = requestResolver;
    }

    public async Task<T?> ExecuteQueryAsync<T>(GraphQlQuery query) where T : class
    {
        var request = QueryRequestBuilder.New()
            .SetQuery(query.Query)
            .SetVariableValues(query.Variables)
            .Create();

        var executor = await _requestResolver.GetRequestExecutorAsync();

        var result = await executor.ExecuteAsync(request);

        return result as T;
    }
    
    public async Task<string?> ExecuteQueryAsyncJsonResult(GraphQlQuery query)
    {
        var request = QueryRequestBuilder.New()
            .SetQuery(query.Query)
            .SetVariableValues(query.Variables)
            .Create();

        var executor = await _requestResolver.GetRequestExecutorAsync();

        var result = await executor.ExecuteAsync(request);

        return result.ToJson();
    }
    
}
