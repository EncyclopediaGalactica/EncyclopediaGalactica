using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

public interface IRelationTypeService
{
    Task<ICollection<RelationTypeResult>> GetAllAsync();
}