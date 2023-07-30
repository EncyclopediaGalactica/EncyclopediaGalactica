namespace IAM.Entities.Interfaces;

public interface IKnowHowCreatedMeAndWhen
{
    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
}