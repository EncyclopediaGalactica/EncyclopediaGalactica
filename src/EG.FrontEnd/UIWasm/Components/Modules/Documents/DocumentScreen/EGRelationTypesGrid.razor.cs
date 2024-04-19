namespace UIWasm.Components.Modules.Documents.DocumentScreen;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Services;

public partial class EGRelationTypesGrid
{
    private FluentDataGrid<RelationTypeResult> Grid;
    private GridItemsProvider<RelationTypeResult> GridItemsProvider;

    [Inject]
    private ILogger<EGRelationTypesGrid> Logger { get; set; }

    [Inject]
    private IRelationTypeService RelationTypeService { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; }

    protected override Task OnParametersSetAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<RelationTypeResult> relationTypeResults = await RelationTypeService.GetAllAsync()
                .ConfigureAwait(false);
            return GridItemsProviderResult.From(
                relationTypeResults,
                relationTypeResults.Count);
        };
        return base.OnParametersSetAsync();
    }

    private async Task HandleOnClickEditAsync(MouseEventArgs _, RelationTypeResult selectedRelationTypeResult)
    {
        await DialogService.ShowDialogAsync<EGEditRelationTypeDialog>(
            selectedRelationTypeResult,
            new DialogParameters
            {
                Height = "400px",
                Title = $"Edit {selectedRelationTypeResult.Name} details",
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleEditDialogSaveAsync),
                PrimaryAction = "Save",
                PrimaryActionEnabled = true
            }
        ).ConfigureAwait(false);
    }

    private async Task HandleEditDialogSaveAsync(DialogResult dialogResult)
    {
        if (dialogResult.Cancelled)
        {
            Logger.LogInformation("Dialog data: {Data}", dialogResult.Data);
        }

        if (dialogResult.Data is not null && !dialogResult.Cancelled)
        {
            Logger.LogInformation("Data is saved.");
            await Grid.RefreshDataAsync();
        }
    }

    private async Task HandleClickOnAddAsync()
    {
        await DialogService.ShowDialogAsync<EGAddRelationTypeDialog>(
            null,
            new DialogParameters
            {
                Height = "400px",
                Width = "1000px",
                Title = $"Create a new Relation Type",
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleAddRelationTypeSaveAsync),
                PrimaryAction = "Save",
                PrimaryActionEnabled = true
            });
    }

    private Task HandleAddRelationTypeSaveAsync(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            // Logger.LogInformation();
        }

        return null;
    }
}