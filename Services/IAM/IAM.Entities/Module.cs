namespace IAM.Entities;

using Interfaces;

public class Module : IHaveId, IKnowWhoModifiedMeAndWhen, IKnowWhoCreatedMeAndWhen
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime LastModified { get; set; }
    public long LastModifiedBy { get; set; }
    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
}