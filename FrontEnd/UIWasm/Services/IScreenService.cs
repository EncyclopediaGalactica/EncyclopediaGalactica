namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IScreenService
{
    Task<IEnumerable<ScreenResult>> GetAll();

    IEnumerable<ScreenResult> GetScreensOfModule(long moduleId);
}