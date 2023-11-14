namespace IAM.Entities.Interfaces;

/// <summary>
/// Interface providing fields for domain entities related to creation.
/// </summary>
public interface IKnowWhoCreatedMeAndWhen
{
    /// <summary>
    /// Property containing the creation date.
    /// </summary>
    DateTime Created { get; set; }

    /// <summary>
    /// Property containing unique identifier of a user who created the entity.
    /// </summary>
    long CreatedBy { get; set; }
}