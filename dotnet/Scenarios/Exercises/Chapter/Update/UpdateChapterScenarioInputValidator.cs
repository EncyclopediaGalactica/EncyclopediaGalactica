namespace EncyclopediaGalactica.Scenarios.Exercises.Chapter.Update;

using Common;
using FluentValidation;
using FluentValidation.Results;
using Logic.Repository.Models;
using static Prelude;

public class UpdateChapterScenarioInputValidator : AbstractValidator<ChapterEntity>
{
    public UpdateChapterScenarioInputValidator()
    {
        RuleFor(r => r.Id).GreaterThanOrEqualTo(1);
        When(
            w => string.IsNullOrEmpty(w.Title) && string.IsNullOrWhiteSpace(w.Title),
            () =>
            {
                RuleFor(e => e.Title.Trim().Length).GreaterThan(3);
            }
        );
        When(
            w => string.IsNullOrEmpty(w.Reference) && string.IsNullOrWhiteSpace(w.Reference),
            () =>
            {
                RuleFor(e => e.Reference.Trim().Length).GreaterThan(3);
            }
        );
        RuleFor(e => e.PageStart).GreaterThan(0);
        RuleFor(e => e.PageEnd).GreaterThan(0);
        RuleFor(e => e.BookId).GreaterThan(0);
    }

    public Either<EgError, ChapterEntity> IsValid(ChapterEntity chapter)
    {
        ValidationResult? result = Validate(chapter);
        if (!result.IsNull() && !result.IsValid)
        {
            return Left(new EgError($"{nameof(ChapterEntity)} is invalid."));
        }

        return Right(chapter);
    }
}