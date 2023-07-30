namespace IAM.Entities;

using Interfaces;

public class User : IHaveId, IKnowWhoModifiedMeAndWhen, IKnowHowCreatedMeAndWhen
{
    public long Id { get; set; }
    public UserName UserName { get; set; }
    public DateTime LastModified { get; set; }
    public long LastModifiedBy { get; set; }
    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
}