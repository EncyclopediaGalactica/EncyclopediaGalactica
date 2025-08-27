namespace EncyclopediaGalactica.Storage.Repository.EdgeType;

using Common;
using Entities;

public partial class EdgeTypeRepository
{
    public Either<EgError, List<EdgeTypeEntity>> GetAll(StorageContext ctx)
    {
        try
        {
            return Right(ctx.EdgeTypes.ToList());
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}