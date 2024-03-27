namespace UIWasm.Layout;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Fluxor;
using Microsoft.AspNetCore.Components;
using UI.States;

public partial class EGHeader
{
    private List<ModuleResult> _moduleResults = new List<ModuleResult>
    {
        new ModuleResult { Id = 0, Name = "Administration", Description = "Administration" },
        new ModuleResult { Id = 1, Name = "Documents", Description = "Documents" },
        new ModuleResult { Id = 2, Name = "Finance", Description = "Finance" },
        new ModuleResult { Id = 3, Name = "StarMap", Description = "StarMap" },
    };

    private ModuleResult? SelectedModule;

    [Inject]
    public IState<ModuleAndScreenState> ModuleState { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    private ILogger<EGHeader> Logger { get; set; }

    private async Task ModuleSelectionChanged(ModuleResult args)
    {
        Logger.LogInformation("===> change: Module selected. The selected module id: {Id}, ", args.Id);
        Dispatcher.Dispatch(new ChangeModuleAndScreenStateAction { ModuleId = args.Id });
    }
}