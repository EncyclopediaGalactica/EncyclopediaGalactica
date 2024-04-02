namespace UIWasm.Services;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public interface IScreenService
{
    Task<IEnumerable<ScreenResult>> GetAll();

    IEnumerable<ScreenResult> GetScreensOfModule(long moduleId);
}