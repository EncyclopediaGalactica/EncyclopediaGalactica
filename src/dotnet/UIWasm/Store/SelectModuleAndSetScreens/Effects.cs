namespace UIWasm.Store.SelectModuleAndSetScreens;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Fluxor;
using Services;

public class Effects(IScreenService screenService, ILogger<Effects> logger)
{
    [EffectMethod]
    public async Task HandleFetchModuleDataAction(ModuleIsSelectedAction action, IDispatcher dispatcher)
    {
        logger.LogInformation("Selected module is {ModuleName}", action.SelectedModule.Name);
        IEnumerable<ScreenResult> screensOfModule = screenService.GetScreensOfModule(action.SelectedModule.Id);
        dispatcher.Dispatch(new ScreensForModuleFetchedResultAction(screensOfModule));
    }
}