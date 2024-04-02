namespace UI.Components;

using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.States;

public partial class EGLeftHandMenu
{
    [Inject]
    public IState<ModuleState> ModuleState { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}