namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;

using Commands.DocumentType;
using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;

/// <summary>
///     Get the list of <see cref="DocumentType" /> entites.
///     <remarks>
///         The scenario provides a single method Api to safely execute the operation for the list of available
///         <see cref="DocumentType" /> entities.
///     </remarks>
/// </summary>
/// <param name="getDocumentTypesCommand"><see cref="IGetDocumentTypesCommand" /> implelemtation.</param>
public class GetDocumentTypesScenario(
    GetDocumentTypesCommand getDocumentTypesCommand)
{
    public async Task<Option<List<DocumentTypeResult>>> ExecuteAsync(
        ISagaContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Option<List<DocumentTypeResult>> result = await getDocumentTypesCommand.ExecuteAsync(cancellationToken)
                .ConfigureAwait(false);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}