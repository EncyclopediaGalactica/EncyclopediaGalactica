namespace UIWasm.Components.Modules.Documents.DocumentScreen;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Services;

public partial class EGDocumentsScreen
{
    private FluentDataGrid<DocumentResult> Grid;
    private GridItemsProvider<DocumentResult> GridItemsProvider;

    [Inject]
    private ILogger<EGDocumentsScreen> Logger { get; set; }

    [Inject]
    private IDocumentService DocumentService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<DocumentResult> r = await DocumentService.GetAll().ConfigureAwait(false);
            return GridItemsProviderResult.From<DocumentResult>(
                r,
                r.Count);
        };
        await base.OnInitializedAsync();
    }
}