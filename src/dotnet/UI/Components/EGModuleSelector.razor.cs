namespace UI.Components;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.Actions;
using Store.States;

public partial class EGModuleSelector
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
    public IState<ModuleState> ModuleState { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    private ILogger<EGModuleSelector> Logger { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("===> Init: Module name: {ModuleName}", nameof(EGModuleSelector));
        Logger.LogInformation("===> Init: Selected module is: {Module}", SelectedModule);
        await base.OnInitializedAsync();
    }

    private async Task ModuleSelectionChanged(ChangeEventArgs args)
    {
        Logger.LogInformation("===> change: Module selected. The selected module is: {Value}", args.Value);
        Logger.LogInformation("===> change: Selected module is: {Module}", SelectedModule);
        Console.WriteLine("Whatever");
        Dispatcher.Dispatch(new ChangeModuleAction { ModuleId = 2 });
    }
}