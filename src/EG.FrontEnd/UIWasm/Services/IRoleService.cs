namespace UIWasm.Services;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public interface IRoleService
{
    Task<ICollection<RoleResult>> GetAll();
}