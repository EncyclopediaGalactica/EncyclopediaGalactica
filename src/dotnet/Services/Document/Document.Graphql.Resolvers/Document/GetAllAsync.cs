namespace EncyclopediaGalactica.Services.Document.Graphql.Resolvers.Document;

using Contracts.Input;
using Contracts.Output;
using HotChocolate.Resolvers;
using Service.Interfaces.Document;

public partial class DocumentResolvers
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
        return await getAllDocumentsScenario.GetAllAsync();
    }
}