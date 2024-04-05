namespace UIWasm.Components.Modules.Documents;

using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.SelectModuleAndSetScreens;

public partial class EGDocumentsMainContent
{
    [Inject]
    private ILogger<EGDocumentsMainContent> Logger { get; set; }

    [Inject]
    private IState<ModuleAndScreenState> ModuleAndScreenState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}