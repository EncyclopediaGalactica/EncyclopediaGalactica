namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Resolvers.QueryResolvers;

using Contracts.Input;
using Contracts.Output;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;
using Scenario.Interfaces.Document;

public class DocumentQueryResolvers(ILogger<DocumentQueryResolvers> logger)
{
    /// <summary>
    ///     Returns a list of <see cref="DocumentInput" /> representing <see cref="resolverContext" /> entities of the
    ///     system.
    /// </summary>
    /// <param name="resolverContext">
    ///     <see cref="getAllDocumentsScenario" />
    /// </param>
    /// <param name="getAllDocumentsScenario">
    ///     <see cref="Task{TResult}" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Document" /> representing result of asynchronous operation.
    /// </returns>
    public async Task<IList<DocumentResult>> GetAllAsync(
        IResolverContext resolverContext,
        IGetAllDocumentsScenario getAllDocumentsScenario)
    {
        try
        {
            return await getAllDocumentsScenario.GetAllAsync();
        }
        catch (Exception e)
        {
            logger.LogDebug("{OperationName} has failed. Message: {Message}; Stacktrace: {StackTrace}",
                nameof(GetAllAsync),
                e.Message,
                e.StackTrace);
            Console.WriteLine(e);
            throw;
        }
    }
}