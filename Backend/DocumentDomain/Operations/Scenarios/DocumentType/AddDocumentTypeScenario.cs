namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;

using Commands.DocumentType;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;

/// <summary>
///     Adding a <see cref="DocumentType" /> entity to the system.
/// </summary>
/// <remarks>
///     This scenario includes all the necessary steps and procedures to add safely a new <see cref="DocumentType" />
///     entity to the system.
/// </remarks>
/// <param name="addDocumentTypeCommand">
///     <see cref="IAddDocumentTypeCommand" /> implementation.
/// </param>
public class AddDocumentTypeScenario(IAddDocumentTypeCommand addDocumentTypeCommand)
{
    public async Task<Option<DocumentTypeResult>> ExecuteAsync(
        AddDocumentTypeScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ExecuteOperationAsync(context.Payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<Option<DocumentTypeResult>> ExecuteOperationAsync(DocumentTypeInput? payload,
        CancellationToken cancellationToken)
    {
        return await addDocumentTypeCommand.ExecuteAsync(payload, cancellationToken).ConfigureAwait(false);
    }
}