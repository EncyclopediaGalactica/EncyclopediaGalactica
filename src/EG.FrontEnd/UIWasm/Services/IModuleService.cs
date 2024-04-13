namespace UIWasm.Services;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public interface IModuleService
{
    IEnumerable<ModuleResult> GetAll();
}