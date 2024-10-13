namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IApplicationService
{
    Task<ICollection<ApplicationResult>> GetAllAsync();
}