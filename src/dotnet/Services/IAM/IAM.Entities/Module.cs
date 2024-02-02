namespace IAM.Entities;

using Interfaces;

/// <summary>
///     Module entity.
///     <para>
///         A Module is a set of functionalities of the system. In case of IAM it means available functionalities.
///     </para>
/// </summary>
public class Module : IHaveId, IKnowWhoModifiedMeAndWhen, IKnowWhoCreatedMeAndWhen
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long Id { get; set; }
    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
    public DateTime LastModified { get; set; }
    public long LastModifiedBy { get; set; }
}