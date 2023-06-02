namespace EncyclopediaGalactica.Services.Document.Sdk.Interfaces;

/// <summary>
///     The ISourceFormatsSdk interface.
///     <remarks>
///         It includes all domain specific DocumentClient interfaces.
///     </remarks>
/// </summary>
public interface ISourceFormatsSdk
{
    /// <summary>
    /// Gets the <see cref="ISourceFormatNodeSdk"/> DocumentClient.
    /// </summary>
    ISourceFormatNodeSdk SourceFormatNode { get; }

    /// <summary>
    /// Gets the <see cref="IDocumentsSdk"/> DocumentClient.
    /// </summary>
    IDocumentsSdk DocumentsSdk { get; }
}