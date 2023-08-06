namespace IAM.Entities.Interfaces;

public interface IKnowWhoModifiedMeAndWhen
{
    public DateTime LastModified { get; set; }
    public long LastModifiedBy { get; set; }
}