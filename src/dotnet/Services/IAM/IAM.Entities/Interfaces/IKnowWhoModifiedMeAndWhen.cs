namespace IAM.Entities.Interfaces;

/// <summary>
/// Interface providing fields regarding entity modification information.
/// </summary>
public interface IKnowWhoModifiedMeAndWhen
{
    /// <summary>
    /// Field containing the last modification date of the entity.
    /// </summary>
    DateTime LastModified { get; set; }

    /// <summary>
    /// Property containing the unique identifier of the user modified the entity last time.
    /// </summary>
    long LastModifiedBy { get; set; }
}