namespace UIWasm.Store.SelectModuleAndSetScreens;

using EncyclopediaGalactica.Common.Contracts;

public class ScreensForModuleFetchedResultAction(IEnumerable<ScreenResult>? screensOfAModule)
{
    public IEnumerable<ScreenResult>? ScreensOfAModule { get; } = screensOfAModule;
}