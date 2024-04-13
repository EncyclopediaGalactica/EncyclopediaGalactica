namespace EncyclopediaGalactica.BusinessLogic.Validators;

public static class ValidationMessages
{
    public static readonly string StringFieldMustNotBeEmpty = "Field must not be empty.";
    public static readonly string StringFieldLengthMustBe3OrGreater = "Field length must be 3 chars or more.";
    public static readonly string IdMustBeZeroWhenAddingNewEntity = "Id value must be zero when adding new entity";
}