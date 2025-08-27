namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Chapter;

using Common;
using Microsoft.EntityFrameworkCore;
using Models;
using static Prelude;

public partial class ChapterRepository
{
    public Either<EgError, ChapterEntity> Update(
        ChapterEntity entity,
        ExercisesContext ctx
    )
    {
        try
        {
            ChapterEntity target = ctx.Chapters
                .First(id => id.Id == entity.Id);
            target.Title = entity.Title;
            target.BookId = entity.BookId;
            target.PageEnd = entity.PageEnd;
            target.PageStart = entity.PageStart;
            target.Reference = entity.Reference;
            ctx.Entry(target).State = EntityState.Modified;
            ctx.SaveChanges();
            return Right(target);
        }
        catch (Exception e)
        {
            return Left(
                new EgError($"Error happened while updating {nameof(ChapterEntity)} with id: {entity.Id}.")
            );
        }
    }
}