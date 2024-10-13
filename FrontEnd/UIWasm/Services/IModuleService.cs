namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IModuleService
{
    IEnumerable<ModuleResult> GetAll();
}