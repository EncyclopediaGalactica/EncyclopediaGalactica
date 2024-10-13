namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IRoleService
{
    Task<ICollection<RoleResult>> GetAll();
}