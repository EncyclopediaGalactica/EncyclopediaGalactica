namespace EncyclopediaGalactica.Storage.Repository.Edge;

using Common;
using Entities;

public partial class EdgeRepository
{
    public Either<EgError, List<EdgeEntity>> GetAll(StorageContext ctx)
    {
        try
        {
            List<EdgeEntity> result = ctx.Edges.ToList();
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}