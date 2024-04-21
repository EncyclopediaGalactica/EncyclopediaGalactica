using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

namespace UIWasm.Components.Modules.Documents.DocumentStructuresGrid;

public partial class EGDocumentStructuresGrid
{
    private FluentDataGrid<DocumentResult> Grid;
    private GridItemsProvider<DocumentResult> GridItemsProvider;

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
            ICollection<DocumentResult> r = await DocumentStructureService.GetAllAsync().ConfigureAwait(false);
            return GridItemsProviderResult.From<DocumentResult>(
                r,
                r.Count);
        };
        await base.OnInitializedAsync();
    }
}