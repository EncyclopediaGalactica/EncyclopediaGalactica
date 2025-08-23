namespace EncyclopediaGalactica.Scenarios.Exercises.Book.AddNew;

using Common;
using FluentValidation;
using FluentValidation.Results;
using Logic.Repository.Models;
using static Prelude;

public class AddNewBookScenarioInputValidator : AbstractValidator<BookEntity>
{
    public AddNewBookScenarioInputValidator()
    {
        RuleFor(r => r.Id).Equal(0);
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

    public Either<EgError, BookEntity> IsValid(BookEntity bookEntity)
    {
        ValidationResult? validationResult = Validate(bookEntity);
        if (validationResult.IsValid)
        {
            return Right<EgError, BookEntity>(bookEntity);
        }

        return Left<EgError, BookEntity>(validationResult.ToEgError());
    }
}