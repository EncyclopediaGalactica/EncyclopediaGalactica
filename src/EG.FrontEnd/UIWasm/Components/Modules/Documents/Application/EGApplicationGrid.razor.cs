using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

namespace UIWasm.Components.Modules.Documents.Application;

public partial class EGApplicationGrid
{
    [Inject]
    private ILogger<EGApplicationGrid> Logger { get; set; }

    [Inject]
    private IApplicationService ApplicationService { get; set; }

    private FluentDataGrid<ApplicationResult> Grid;
    private GridItemsProvider<ApplicationResult> GridItemsProvider;

    protected override Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<ApplicationResult> r = await ApplicationService.GetAllAsync().ConfigureAwait(false);
            return GridItemsProviderResult.From<ApplicationResult>(
                r,
                r.Count);
        };
        return base.OnInitializedAsync();
    }
}