namespace UIWasm.Components;

using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.SelectModuleAndSetScreens;

public partial class EGLeftHandMenu
{
    private bool expanded = true;

    [Inject]
    private IState<ModuleState> ModuleAndScreenState { get; set; }

    [Inject]
    private ILogger<EGLeftHandMenu> Logger { get; set; }

    protected override void OnInitialized()
    {
        Logger.LogInformation("Init");
        base.OnInitialized();
    }
}