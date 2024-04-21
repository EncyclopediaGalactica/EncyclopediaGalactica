using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

namespace UIWasm.Components.Modules.Documents.DocumentGrid;

public partial class EGDocumentGrid
{
    [Inject]
    public IDocumentService DocumentService { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; }

    private FluentDataGrid<DocumentResult> Grid;
    private GridItemsProvider<DocumentResult> GridItemsProvider;

    protected async override Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<DocumentResult> r = await DocumentService.GetAllAsync().ConfigureAwait(false);
            return GridItemsProviderResult.From<DocumentResult>(
                r,
                r.Count);
        };
    }

    private async Task HandlePreviewOnClickAsync(MouseEventArgs _, DocumentResult context)
    {
        await DialogService.ShowDialogAsync<EGPreviewDocumentDialog>(
            new DocumentResult(),
            new DialogParameters
            {
                Height = "400px",
                Width = "600px",
                PreventScroll = true,
                PreventDismissOnOverlayClick = true,
            });
    }

    private async Task HandleDeleteOnClickAsync(MouseEventArgs _, DocumentResult context)
    {
        await DialogService.ShowDialogAsync<EGPreviewDocumentDialog>(
            new DocumentResult(),
            new DialogParameters
            {
                Height = "400px",
                Width = "600px",
                PreventScroll = true,
                PreventDismissOnOverlayClick = true,
            });
    }
}