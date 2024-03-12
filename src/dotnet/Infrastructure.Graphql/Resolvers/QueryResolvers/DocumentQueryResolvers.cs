namespace EncyclopediaGalactica.Infrastructure.Graphql.Resolvers.QueryResolvers;

using BusinessLogic.Commands.Document;
using BusinessLogic.Contracts;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;

public class DocumentQueryResolvers(ILogger<DocumentQueryResolvers> logger)
{
    /// <summary>
    ///     Returns a list of <see cref="DocumentInput" /> representing <see cref="resolverContext" /> entities of the
    ///     system.
    /// </summary>
    /// <param name="resolverContext">
    ///     <see cref="getAllDocumentsCommand" />
    /// </param>
    /// <param name="getAllDocumentsCommand">
    ///     <see cref="Task{TResult}" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Document" /> representing result of asynchronous operation.
    /// </returns>
    public async Task<IList<DocumentResult>> GetAllAsync(
        IResolverContext resolverContext,
        IGetAllDocumentsCommand getAllDocumentsCommand)
    {
        try
        {
            return await getAllDocumentsCommand.GetAllAsync();
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