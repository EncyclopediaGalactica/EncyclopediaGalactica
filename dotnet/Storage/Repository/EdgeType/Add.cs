namespace EncyclopediaGalactica.Storage.Repository.EdgeType;

using Common;
using Entities;

public partial class EdgeTypeRepository
{
    public Either<EgError, EdgeTypeEntity> Add(EdgeTypeEntity input, StorageContext ctx)
    {
        try
        {
            ctx.EdgeTypes.Add(input);
            ctx.SaveChanges();
            return Right(input);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}