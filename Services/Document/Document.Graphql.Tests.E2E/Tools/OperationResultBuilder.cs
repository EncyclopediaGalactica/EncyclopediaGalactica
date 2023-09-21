namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tools;

using HotChocolate.Execution;
using Newtonsoft.Json;

public class OperationResultBuilder
{
    public string? QueryResultString { get; set; }
    public string? Path { get; set; }

    public T Build<T>()
    {
        if (string.IsNullOrEmpty(QueryResultString) || string.IsNullOrWhiteSpace(QueryResultString))
        {
            string msg = $"{nameof(QueryResultString)} is empty, null or whitespace";
            throw new ArgumentException(msg);
        }

        IQueryResult? result = JsonConvert.DeserializeObject<QueryResult>(QueryResultString);

        if (result is null || result.Data is null || !result.Data.Any())
        {
            string msg = $"Result serialization did not result data";
            throw new ArgumentNullException(msg);
        }

        string dataResult = JsonConvert.SerializeObject(result.Data[Path]);

        if (string.IsNullOrEmpty(dataResult))
        {
            string msg = $"Data serialization was unsuccessful";
            throw new ArgumentNullException(msg);
        }

        T? deserializedResult = JsonConvert.DeserializeObject<T>(dataResult);

        if (deserializedResult is null)
        {
            string msg = $"Actual data deserialization resulted in null.";
            throw new ArgumentNullException(msg);
        }

        return deserializedResult;
    }
}