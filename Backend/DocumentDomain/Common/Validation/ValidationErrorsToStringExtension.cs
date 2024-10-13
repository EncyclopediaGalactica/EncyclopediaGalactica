namespace EncyclopediaGalactica.DocumentDomain.Common.Validation;

using System.Text;
using FluentValidation.Results;

public static class ValidationErrorsToStringExtension
{
    public static string ToSummarize(this List<ValidationFailure> validationFailures)
    {
        StringBuilder builder = new();
        validationFailures.ForEach(e =>
            builder
                .Append("Error:")
                .Append(' ')
                .Append("Property name:")
                .Append(' ')
                .Append($"{e.PropertyName}")
                .Append(' ')
                .Append($"error message")
                .Append(' ')
                .Append($"{e.ErrorMessage}")
        );
        return builder.ToString();
    }
}