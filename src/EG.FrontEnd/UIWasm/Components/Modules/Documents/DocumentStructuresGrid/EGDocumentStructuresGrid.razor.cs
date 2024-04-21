using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
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
        throw new NotImplementedException();
    }
}