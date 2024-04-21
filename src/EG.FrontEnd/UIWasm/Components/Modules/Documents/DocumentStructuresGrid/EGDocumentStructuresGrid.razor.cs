using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

namespace UIWasm.Components.Modules.Documents.DocumentStructuresGrid;

public partial class EGDocumentStructuresGrid
{
    private FluentDataGrid<DocumentStructureResult> Grid;
    private GridItemsProvider<DocumentStructureResult> GridItemsProvider;

    [Inject]
    private ILogger<EGDocumentStructuresGrid> Logger { get; set; }

    [Inject]
    private IDocumentStructureService DocumentStructureService { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<DocumentStructureResult> r = await DocumentStructureService.GetAllAsync().ConfigureAwait(false);
            return GridItemsProviderResult.From<DocumentStructureResult>(
                r,
                r.Count);
        };
        await base.OnInitializedAsync();
    }

    private async Task HandleOnClickAsync()
    {
        await DialogService.ShowDialogAsync<EGAddDocumentStructureDialog>(
            new DocumentStructureResult(),
            new DialogParameters
            {
                Height = "400px",
                Width = "600px",
                PreventScroll = true,
                PreventDismissOnOverlayClick = true,
                PrimaryAction = "Save",
                PrimaryActionEnabled = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleAddDocumentStructureResultSave)
            });
    }

    private async Task HandleAddDocumentStructureResultSave(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("save");
        }
    }

    private async Task HandleOnClickPreviewAsync()
    {
        await DialogService.ShowDialogAsync<EGShowDocumentStructurePreviewDialog>(
            new DocumentStructureResult(),
            new DialogParameters
            {
                Height = "400px",
                Width = "600px",
                PreventScroll = true,
                PreventDismissOnOverlayClick = true,
                PrimaryActionEnabled = false,
            });
    }

    private async Task HandleOnClickEditAsync(MouseEventArgs _, DocumentStructureResult context)
    {
        await DialogService.ShowDialogAsync<EGEditDocumentStructureDialog>(
            new DocumentStructureResult(),
            new DialogParameters
            {
                Height = "400px",
                Width = "600px",
                PreventScroll = true,
                PreventDismissOnOverlayClick = true,
                PrimaryAction = "Save",
                PrimaryActionEnabled = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleEditDocumentStructureResultSave)
            });
    }

    private async Task HandleEditDocumentStructureResultSave(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("edit save");
        }
    }

    private async Task HandleOnClickDeleteAsync(MouseEventArgs _, DocumentStructureResult context)
    {
        await DialogService.ShowDialogAsync<EGDeleteDocumentStructureDialog>(
            new DocumentStructureResult(),
            new DialogParameters
            {
                Height = "400px",
                Width = "600px",
                PreventScroll = true,
                PreventDismissOnOverlayClick = true,
                PrimaryAction = "Save",
                PrimaryActionEnabled = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleDeleteDocumentStructureResultSave)
            });
    }

    private async Task HandleDeleteDocumentStructureResultSave(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("edit save");
        }
    }
}