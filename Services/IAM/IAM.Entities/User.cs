namespace IAM.Entities;

using Interfaces;

public class User : IHaveId, IKnowWhoModifiedMeAndWhen, IKnowHowCreatedMeAndWhen
{
    public UserName UserName { get; set; }
    public long Id { get; set; }
    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
    public DateTime LastModified { get; set; }
    public long LastModifiedBy { get; set; }
}