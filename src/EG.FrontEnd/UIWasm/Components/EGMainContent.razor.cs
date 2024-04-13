namespace UIWasm.Components;

using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.SelectModuleAndSetScreens;

public partial class EGMainContent
{
    [Inject]
    private IState<ModuleAndScreenState> ModuleAndScreenState { get; set; }

    [Inject]
    private ILogger<EGMainContent> Logger { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Logger.LogInformation("MainContent {State} value is {Value}",
            nameof(ModuleAndScreenState.Value.SelectedModule),
            ModuleAndScreenState?.Value.SelectedModule);
        // if (ModuleAndScreenState?.Value.SelectedModule is not null)
        // {
        // Logger.LogInformation("Module and screen state: {Module}", ModuleAndScreenState.Value.SelectedModule.Name);
        // }
    }
}