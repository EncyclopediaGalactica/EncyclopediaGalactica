using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

public interface IApplicationService
{
    Task<ICollection<ApplicationResult>> GetAllAsync();
}