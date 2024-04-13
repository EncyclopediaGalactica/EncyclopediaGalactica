namespace UIWasm.Components;

using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Store.SelectModuleAndSetScreens;

public partial class EGLeftHandMenu
{
    private bool expanded = true;

    [Inject]
    private IState<ModuleAndScreenState> ModuleAndScreenState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    private ILogger<EGLeftHandMenu> Logger { get; set; }

    protected override void OnInitialized()
    {
        Logger.LogInformation("Init");
        base.OnInitialized();
    }

    private void OnClick(MouseEventArgs arg, string moduleUnifiedName)
    {
        Logger.LogInformation("Menu clicked: {ScreenName}", moduleUnifiedName);
        Dispatcher.Dispatch(new ScreenIsSelectedAction(moduleUnifiedName));
    }
}