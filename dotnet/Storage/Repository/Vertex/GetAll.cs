namespace EncyclopediaGalactica.Storage.Repository;

using Common;
using Entities;

public partial class VertexRepository
{
    public Either<EgError, IEnumerable<VertexEntity>> GetAll(StorageContext ctx)
    {
        try
        {
            IEnumerable<VertexEntity> result = ctx.Vertices.ToList();
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
    
}