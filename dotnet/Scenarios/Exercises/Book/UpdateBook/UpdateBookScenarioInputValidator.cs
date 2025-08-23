namespace EncyclopediaGalactica.Scenarios.Exercises.Book.UpdateBook;

using Common;
using FluentValidation;
using FluentValidation.Results;
using Logic.Repository.Models;
using static Prelude;

public class UpdateBookScenarioInputValidator : AbstractValidator<BookEntity>
{
    public UpdateBookScenarioInputValidator()
    {
        RuleFor(r => r.Id).GreaterThanOrEqualTo(1);
        RuleFor(r => r.TopicId).GreaterThanOrEqualTo(1);
        When(
            book => book.Title != null,
            () =>
            {
                RuleFor(book => book.Title).NotEmpty();
                RuleFor(book => book.Title.Trim().Length).GreaterThanOrEqualTo(3);
            }
        );
        When(
            book => book.Reference != null,
            () =>
            {
                RuleFor(book => book.Reference).NotEmpty();
                RuleFor(book => book.Reference.Trim().Length).GreaterThanOrEqualTo(3);
            }
        );
        When(
            book => book.Authors != null,
            () =>
            {
                RuleFor(book => book.Authors).NotEmpty();
                RuleFor(book => book.Authors.Trim().Length).GreaterThanOrEqualTo(3);
            }
        );
    }

    public Either<EgError, BookEntity> IsValid(BookEntity input)
    {
        ValidationResult? validationResult = Validate(input);
        if (validationResult.IsValid)
        {
            return Right<EgError, BookEntity>(input);
        }

        return Left<EgError, BookEntity>(validationResult.ToEgError());
    }
}