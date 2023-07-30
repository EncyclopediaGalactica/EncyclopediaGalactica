namespace IAM.Entities;

public class UserName : IHaveId, IKnowWhoModifiedMeAndWhen, IKnowHowCreatedMeAndWhen
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public long Id { get; set; }
    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
    public DateTime LastModified { get; set; }
    public long LastModifiedBy { get; set; }
}