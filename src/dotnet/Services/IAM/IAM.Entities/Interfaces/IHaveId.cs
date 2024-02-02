namespace IAM.Entities.Interfaces;

/// <summary>
/// This interface provides Id for domain entities.
/// </summary>
public interface IHaveId
{
    /// <summary>
    /// Id property for domain entity.
    /// </summary>
    long Id { get; set; }
}