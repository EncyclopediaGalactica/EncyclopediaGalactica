namespace EncyclopediaGalactica.Services.Document.Entities;

/// <summary>
///     Document entity
///     <p>
///         Document entity represents a document and the root of a tree including many objects describing a document in
///         the system.
///     </p>
///     <p>
///         A document can be anything (website, xml file, docx file, md file) which contains some information.
///     </p>
/// </summary>
public class Document
{
    /// <summary>
    ///     Gets os sets the Id value.
    ///     <remarks>
    ///         The id value is a unique identifier within the system.
    ///     </remarks>
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Gets or sets the Name value.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the Description value
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Gets or sets the Uri value
    /// </summary>
    public Uri? Uri { get; set; }
}