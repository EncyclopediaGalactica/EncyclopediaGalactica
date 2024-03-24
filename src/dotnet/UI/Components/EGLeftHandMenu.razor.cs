namespace UI.Components;

using Fluxor;
using Microsoft.AspNetCore.Components;
using States;

public partial class EGLeftHandMenu
{
    [Inject]
    private IState<ModuleState> ModuleState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}