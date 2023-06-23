namespace IAM.Entities;

public interface IKnowHowCreatedMeAndWhen
{
    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
}