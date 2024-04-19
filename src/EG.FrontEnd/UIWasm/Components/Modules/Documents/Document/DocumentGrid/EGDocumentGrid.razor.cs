using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

namespace UIWasm.Components.Modules.Documents.Document.DocumentGrid;

public partial class EGDocumentGrid
{
    private FluentDataGrid<DocumentResult> Grid;
    private GridItemsProvider<DocumentResult> GridItemsProvider;

    [Inject]
    private ILogger<EGDocumentGrid> Logger { get; set; }

    [Inject]
    private IDocumentService DocumentService { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<DocumentResult> r = await DocumentService.GetAllAsync().ConfigureAwait(false);
            return GridItemsProviderResult.From<DocumentResult>(
                r,
                r.Count);
        };
        await base.OnInitializedAsync();
    }
}