namespace UIWasm.Components.Modules.Administration;

using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.SelectModuleAndSetScreens;

public partial class EGAdministrationMainContent
{
    [Inject]
    private IState<ModuleAndScreenState>? ModuleAndScreenState { get; set; }

    [Inject]
    private ILogger<EGAdministrationMainContent> Logger { get; set; }

    protected override void OnInitialized()
    {
        Logger.LogInformation("Init");
        base.OnInitialized();
    }
}