namespace EncyclopediaGalactica.Storage.Repository.EdgeType;

using Common;
using Entities;

public partial class EdgeTypeRepository
{
    public Either<EgError, Option<EdgeTypeEntity>> GetIdByReference(string reference, StorageContext ctx)
    {
        try
        {
            EdgeTypeEntity? hit = ctx.EdgeTypes.FirstOrDefault(e => e.Reference == reference);
            return hit == null ? Option<EdgeTypeEntity>.None : Option<EdgeTypeEntity>.Some(hit);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}