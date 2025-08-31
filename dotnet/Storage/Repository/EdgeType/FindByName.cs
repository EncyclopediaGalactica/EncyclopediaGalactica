namespace EncyclopediaGalactica.Storage.Repository.EdgeType;

using Common;
using Entities;

public partial class EdgeTypeRepository
{
    public Either<EgError, Option<EdgeTypeEntity>> FindByName(string name, StorageContext ctx)
    {
        try
        {
            EdgeTypeEntity? entity = ctx.EdgeTypes.FirstOrDefault(e => e.Name == name);
            return entity == null ? Right(Option<EdgeTypeEntity>.None) : Right(Option<EdgeTypeEntity>.Some(entity));
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}