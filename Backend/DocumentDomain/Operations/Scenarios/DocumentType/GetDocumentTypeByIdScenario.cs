namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;

using Commands.DocumentType;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;

/// <summary>
///     Get the <see cref="DocumentType" /> by Id scenario.
///     <remarks>
///         The scenario provides a single method Api to retrieve the desired <see cref="DocumentType" /> entity
///         representation from the system safely.
///     </remarks>
/// </summary>
/// <param name="getDocumentTypeByIdCommand"><see cref="IGetDocumentTypeByIdCommand" /> implementation.</param>
public class GetDocumentTypeByIdScenario(IGetDocumentTypeByIdCommand getDocumentTypeByIdCommand)
{
    public async Task<Option<DocumentTypeResult>> ExecuteAsync(
        GetDocumentTypeByIdScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await getDocumentTypeByIdCommand.ExecuteAsync(context.Payload, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}