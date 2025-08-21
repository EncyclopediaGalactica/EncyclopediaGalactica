namespace EncyclopediaGalactica.Scenarios;

using Common;
using FluentValidation.Results;

public static class FluentValidatorExtensions
{
    public static EgError ToEgError(this ValidationResult? validationResult)
    {
        string errorString = string.Join(", ", validationResult!.Errors.Select(x => x.ErrorMessage).ToList());
        return new EgError(errorString);
    }
}