using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

namespace UIWasm.Components.Modules.Documents.DocumentTypeGrid;

public partial class EGDocumentTypeGrid
{
    [Inject]
    private ILogger<EGDocumentTypeGrid> Logger { get; set; }

    [Inject]
    private IDocumentTypeService DocumentTypeService { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; }

    private FluentDataGrid<DocumentTypeResult> Grid;
    private GridItemsProvider<DocumentTypeResult> GridItemsProvider;

    protected override async Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<DocumentTypeResult> r = await DocumentTypeService.GetAllAsync().ConfigureAwait(false);
            return GridItemsProviderResult.From(
                r,
                r.Count);
        };
        await base.OnInitializedAsync();
    }

    private async Task HandleAddOnClickAsync()
    {
        await DialogService.ShowDialogAsync<EGAddDocumentTypeDialog>(
                new DocumentTypeResult(),
                new DialogParameters
                {
                    Height = "400px",
                    Width = "600px",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    PrimaryAction = "Save",
                    PrimaryActionEnabled = true,
                    OnDialogResult = DialogService.CreateDialogCallback(this, HandleAddSaveAsync)
                })
            .ConfigureAwait(false);
    }

    private async Task HandleAddSaveAsync(DialogResult arg)
    {
        if (arg is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("save");
        }
    }

    private async Task HandleEditOnClickAsync(MouseEventArgs _, DocumentTypeResult documentTypeResult)
    {
        await DialogService.ShowDialogAsync<EGEditDocumentTypeDialog>(
            documentTypeResult,
            new DialogParameters
            {
                Height = "400px",
                Width = "600px",
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
                PrimaryAction = "Save",
                PrimaryActionEnabled = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleEditSaveAsync)
            });
    }

    private async Task HandleEditSaveAsync(DialogResult arg)
    {
        if (arg is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("save");
        }
    }

    private async Task HandleDeleteOnClickAsync(MouseEventArgs mouseEventArgs, DocumentTypeResult type)
    {
        await DialogService.ShowDialogAsync<EGEditDocumentTypeDialog>(
            type,
            new DialogParameters
            {
                Height = "400px",
                Width = "600px",
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
                PrimaryAction = "Save",
                PrimaryActionEnabled = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleEditSaveAsync)
            });
    }
}