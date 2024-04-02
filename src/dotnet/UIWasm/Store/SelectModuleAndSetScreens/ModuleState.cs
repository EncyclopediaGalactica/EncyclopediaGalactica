namespace UIWasm.Store.SelectModuleAndSetScreens;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Fluxor;

[FeatureState]
public class ModuleState
{
    private ModuleState()
    {
    }

    public ModuleState(ModuleResult? selectedModule, IEnumerable<ScreenResult>? screensOfModule, bool isLoading)
    {
        SelectedModule = selectedModule;
        ScreensOfModule = screensOfModule;
        IsLoading = isLoading;
    }

    public ModuleResult? SelectedModule { get; }
    public bool IsLoading { get; }
    public IEnumerable<ScreenResult>? ScreensOfModule { get; }
}