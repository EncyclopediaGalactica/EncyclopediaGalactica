namespace EncyclopediaGalactica.Scenarios.Storage.Vertex;

using EncyclopediaGalactica.Storage.Entities;

public record VertexDto(long Id, Dictionary<string, object> Data);

public static class VertexEntityExtensions
{
    public static VertexDto ToVertexDto(this VertexEntity vertexEntity)
        => new VertexDto(vertexEntity.Id, vertexEntity.Data);

    public static VertexEntity ToEntity(this VertexDto vertexDto) => new VertexEntity
    {
        Id = vertexDto.Id,
        Data = vertexDto.Data
    };

}