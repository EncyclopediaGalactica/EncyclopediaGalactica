namespace EncyclopediaGalactica.Storage.Repository.Vertex;

using Common;
using Entities;

public partial class VertexRepository
{
    public Either<EgError, VertexEntity> AddWithEdgeType(
        VertexEntity input,
        EdgeTypeEntity edgeType,
        StorageContext ctx
    )
    {
        try
        {
            ctx.EdgeTypes.Attach(edgeType);
            input.EdgeTypes.Add(edgeType);
            ctx.Vertices.Add(input);
            return Right(input);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}