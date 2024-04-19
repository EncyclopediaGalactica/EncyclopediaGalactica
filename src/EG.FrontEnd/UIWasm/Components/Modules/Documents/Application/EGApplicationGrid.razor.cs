using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

namespace UIWasm.Components.Modules.Documents.Application;

public partial class EGApplicationGrid
{
    [Inject]
    private ILogger<EGApplicationGrid> Logger { get; set; }

    [Inject]
    private IApplicationService ApplicationService { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; }

    private FluentDataGrid<ApplicationResult> Grid;
    private GridItemsProvider<ApplicationResult> GridItemsProvider;

    protected override Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<ApplicationResult> r = await ApplicationService.GetAllAsync().ConfigureAwait(false);
            return GridItemsProviderResult.From<ApplicationResult>(
                r,
                r.Count);
        };
        return base.OnInitializedAsync();
    }

    private async Task HandleAddClick()
    {
        DialogService.ShowDialogAsync<EGAddApplicationDialog>(
            new ApplicationResult(),
            new DialogParameters
            {
                DialogType = DialogType.Dialog,
                Title = "Add new Application",
                Width = "400px",
                Height = "400px",
                PrimaryAction = "Save",
                PrimaryActionEnabled = true,
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleAddApplicationSave)
            });
    }

    private async Task HandleAddApplicationSave(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("Save");
        }
    }
}