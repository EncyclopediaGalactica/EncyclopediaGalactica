namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Section;

using System.Collections.Immutable;
using Common;
using Microsoft.EntityFrameworkCore;
using Models;

public partial class SectionRepository
{
    public Either<EgError, ImmutableList<SectionEntity>> FindAllAndChapterAndBookAndTopic(ExercisesContext ctx)
    {
        try
        {
            ImmutableList<SectionEntity> sections = ctx.Sections
                .Include(i => i.Chapter)
                .ThenInclude(ii => ii.Book)
                .ThenInclude(iii => iii.Topic)
                .ToImmutableList();
            return Right(sections);
        }
        catch (Exception ex)
        {
            return Left(new EgError(ex.Message, ex.StackTrace));
        }
    }
}