#region

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

#endregion

namespace UIWasm.Components.Modules.Documents.RelationGrid;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGRelationGrid
{
    private FluentDataGrid<RelationResult> Grid;
    private GridItemsProvider<RelationResult> GridItemsProvider;

    [Inject]
    private ILogger<EGRelationGrid> Logger { get; set; }

    [Inject]
    private IRelationService RelationService { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<RelationResult> r = await RelationService.GetAllAsync().ConfigureAwait(false);
            return GridItemsProviderResult.From<RelationResult>(r, r.Count);
        };
    }

    private async Task HandleClickOnAddAsync()
    {
        await DialogService.ShowDialogAsync<EGAddRelationDialog>(
            new RelationResult(),
            new DialogParameters
            {
                Width = "600px",
                Height = "400px",
                PrimaryAction = "Save",
                PrimaryActionEnabled = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleAddSaveAsync),
                PreventScroll = true,
                PreventDismissOnOverlayClick = true
            }).ConfigureAwait(false);
    }

    private async Task HandleAddSaveAsync(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("Add is saved");
        }
    }

    private async Task HandleOnClickEditAsync(MouseEventArgs mouseEventArgs, RelationResult context)
    {
        await DialogService.ShowDialogAsync<EGEditRelationDialog>(
            context,
            new DialogParameters
            {
                Width = "600px",
                Height = "400px",
                PrimaryAction = "Delete",
                PrimaryActionEnabled = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleEditSaveAsync),
                PreventScroll = true,
                PreventDismissOnOverlayClick = true
            }).ConfigureAwait(false);
    }

    private async Task HandleEditSaveAsync(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("Edit is saved");
        }
    }

    private async Task HandleOnClickDeleteAsync(MouseEventArgs mouseEventArgs, RelationResult context)
    {
        await DialogService.ShowDialogAsync<EGDeleteRelationDialog>(
            context,
            new DialogParameters
            {
                Width = "600px",
                Height = "400px",
                PrimaryAction = "Delete",
                PrimaryActionEnabled = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleDeleteSaveAsync),
                PreventScroll = true,
                PreventDismissOnOverlayClick = true
            }).ConfigureAwait(false);
    }

    private async Task HandleDeleteSaveAsync(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("Delete is saved");
        }
    }
}