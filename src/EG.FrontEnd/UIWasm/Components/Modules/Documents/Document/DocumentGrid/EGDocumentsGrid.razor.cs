namespace UIWasm.Components.Modules.Documents.Document.DocumentGrid;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Services;

public partial class EGDocumentsGrid
{
    private FluentDataGrid<DocumentResult> Grid;
    private GridItemsProvider<DocumentResult> GridItemsProvider;

    [Inject]
    private ILogger<EGDocumentsGrid> Logger { get; set; }

    [Inject]
    private IDocumentService DocumentService { get; set; }

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