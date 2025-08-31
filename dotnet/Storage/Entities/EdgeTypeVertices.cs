namespace EncyclopediaGalactica.Storage.Entities;

public class EdgeTypeVertices
{
    public long EdgeTypeId { get; set; }
    public long VertexId { get; set; }
    public EdgeTypeEntity EdgeType { get; set; } = null!;
    public VertexEntity Vertex { get; set; } = null!;
}