namespace UIWasm.Store.SelectModuleAndSetScreens;

using EncyclopediaGalactica.Common.Contracts;
using Fluxor;

[FeatureState]
public class ModuleAndScreenState
{
    private ModuleAndScreenState()
    {
    }

    public ModuleAndScreenState(
        ModuleResult? selectedModule,
        IEnumerable<ScreenResult>? screensOfModule,
        string? selectedScreen,
        bool isLoading)
    {
        SelectedModule = selectedModule;
        ScreensOfModule = screensOfModule;
        SelectedScreen = selectedScreen;
        IsLoading = isLoading;
    }

    public ModuleResult? SelectedModule { get; }
    public string? SelectedScreen { get; set; }
    public bool IsLoading { get; }
    public IEnumerable<ScreenResult>? ScreensOfModule { get; }
}