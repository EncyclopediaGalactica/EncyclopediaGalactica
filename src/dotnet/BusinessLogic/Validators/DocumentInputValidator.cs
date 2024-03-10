namespace EncyclopediaGalactica.BusinessLogic.Validators;

public class DocumentInputValidator : AbstractValidator<DocumentInput>
{
    public enum Scenarios
    {
        AddNew,
        Update
    }

    public DocumentInputValidator()
    {
        RuleSet(Scenarios.AddNew.ToString(), () =>
        {
            RuleFor(p => p.Id).Equal(0)
                .WithMessage(ValidationMessages.IdMustBeZeroWhenAddingNewEntity);
            RuleFor(p => p.Name).NotNull().NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            When(prop => !string.IsNullOrEmpty(prop.Name), () =>
            {
                RuleFor(p => p.Name.Trim().Length).GreaterThanOrEqualTo(3)
                    .WithMessage(ValidationMessages.StringFieldLengthMustBe3OrGreater);
            });
            When(prop => !string.IsNullOrEmpty(prop.Description), () =>
            {
                RuleFor(p => p.Description.Trim()).NotEmpty()
                    .WithMessage(ValidationMessages.StringFieldMustNotBeEmpty);
            });
        });

        RuleSet(Scenarios.Update.ToString(), () =>
        {
            RuleFor(p => p.Id).GreaterThanOrEqualTo(1);
            RuleFor(p => p.Name).NotNull().NotEmpty();
            RuleFor(p => p.Description).NotNull().NotEmpty();
            When(prop => !string.IsNullOrEmpty(prop.Name), () =>
            {
                RuleFor(p => p.Name.Trim().Length).GreaterThanOrEqualTo(3)
                    .WithMessage(ValidationMessages.StringFieldLengthMustBe3OrGreater);
            });
            When(prop => !string.IsNullOrEmpty(prop.Description), () =>
            {
                RuleFor(p => p.Description.Trim().Length).NotEmpty()
                    .WithMessage(ValidationMessages.StringFieldMustNotBeEmpty);
            });
        });
    }
}