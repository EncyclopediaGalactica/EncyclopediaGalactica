namespace EncyclopediaGalactica.Storage.Repository.Edge;

using Common;
using Entities;

public partial class EdgeRepository
{
    public Either<EgError, EdgeEntity> Add(EdgeEntity input, StorageContext ctx)
    {
        try
        {
            ctx.Add(input);
            ctx.SaveChanges();
            return Right(input);
        }
        catch (Exception e)
        {
            string message = $"{nameof(EdgeRepository)}.{nameof(Add)}: {e.Message}";
            return Left(new EgError(message, e.StackTrace));
        }
    }
}