namespace EncyclopediaGalactica.SourceFormats.ValidatorService;

using Dtos;
using FluentValidation;

public class SourceFormatNodeDtoValidator : AbstractValidator<SourceFormatNodeDto>
{
    public const string Add = "Add";

    public SourceFormatNodeDtoValidator()
    {
        RuleSet(Add, () => { });
    }
}