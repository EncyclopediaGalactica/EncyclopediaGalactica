namespace UIWasm.Layout;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Services;
using Store.SelectModuleAndSetScreens;

public partial class EGHeader
{
    private IEnumerable<ModuleResult> _modules;

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    private ILogger<EGHeader> Logger { get; set; }

    [Inject]
    private IModuleService ModuleService { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitializedAsync();
        _modules = ModuleService.GetAll();
    }

    private void ModuleSelectionChanged(ModuleResult selectedModule)
    {
        Logger.LogInformation("Module selected: Id: {Id}, Name: {Name}. ", selectedModule.Id, selectedModule.Name);
        Dispatcher.Dispatch(new ModuleIsSelectedAction(selectedModule));
    }
}