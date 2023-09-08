namespace Documents.Graphql.Tests.E2E.Tools;

using HotChocolate.Execution;
using Newtonsoft.Json;

public class QueryResult : IQueryResult
{
    public async ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public void RegisterForCleanup(Func<ValueTask> clean)
    {
        throw new NotImplementedException();
    }

    public ExecutionResultKind Kind { get; }
    public IReadOnlyDictionary<string, object?>? ContextData { get; }

    public IReadOnlyDictionary<string, object?> ToDictionary()
    {
        throw new NotImplementedException();
    }

    public string? Label { get; }
    public HotChocolate.Path? Path { get; }

    [JsonProperty("data")]
    public IReadOnlyDictionary<string, object?>? Data { get; set; }

    public IReadOnlyList<object?>? Items { get; }
    public IReadOnlyList<IError>? Errors { get; }
    public IReadOnlyDictionary<string, object?>? Extensions { get; }
    public IReadOnlyList<IQueryResult>? Incremental { get; }
    public bool? HasNext { get; }
}