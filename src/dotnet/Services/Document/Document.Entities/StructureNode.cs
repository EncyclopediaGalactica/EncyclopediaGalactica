namespace EncyclopediaGalactica.Services.Document.Entities;

/// <summary>
///     Structure entity
///     <remarks>
///         <p>It describes a structure of a <see cref="Document" /> entity.</p>
///         <p>
///             It provides a skeleton describing the <see cref="Document" /> structure and by this provides a place to
///             attach entities to their right place to express meaning. Meaning can be information where the meaning is
///             defined by the information itself and by the context its placed in. The other meaning related information
///             is data recording related. For example we know what input validation is needed for Dutch post codes and we
///             can store this with the entity as this information belongs there.
///         </p>
///     </remarks>
/// </summary>
public class StructureNode
{
    /// <summary>
    ///     Gets or sets the Id value.
    /// </summary>
    public long Id { get; set; }

    public Document? Document { get; set; }
    public long DocumentId { get; set; }
    public int IsRootNode { get; set; }
}