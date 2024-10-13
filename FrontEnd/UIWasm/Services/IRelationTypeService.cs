namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IRelationTypeService
{
    Task<ICollection<RelationTypeResult>> GetAllAsync();
}