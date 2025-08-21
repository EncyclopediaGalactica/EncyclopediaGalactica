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
            return Left(new EgError(e.Message));
        }
    }
}