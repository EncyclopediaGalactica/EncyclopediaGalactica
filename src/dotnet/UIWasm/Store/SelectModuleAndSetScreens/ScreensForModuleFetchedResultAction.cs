namespace UIWasm.Store.SelectModuleAndSetScreens;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public class ScreensForModuleFetchedResultAction(IEnumerable<ScreenResult>? screensOfAModule)
{
    public IEnumerable<ScreenResult>? ScreensOfAModule { get; } = screensOfAModule;
}