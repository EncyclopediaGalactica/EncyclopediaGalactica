namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IRelationService
{
    Task<ICollection<RelationResult>> GetAllAsync();
}