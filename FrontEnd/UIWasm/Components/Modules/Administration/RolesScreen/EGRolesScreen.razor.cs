namespace UIWasm.Components.Modules.Administration.RolesScreen;

using EncyclopediaGalactica.Common.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Services;

public partial class EGRolesScreen
{
    private FluentDataGrid<RoleResult> Grid;
    private GridItemsProvider<RoleResult> GridItemsProvider = default!;

    [Inject]
    private ILogger<RoleResult> Logger { get; set; }

    [Inject]
    private IRoleService RoleService { get; set; }


    protected override async Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<RoleResult> r = await RoleService.GetAll();
            return GridItemsProviderResult.From(
                r,
                r.Count);
        };

        await base.OnInitializedAsync();
    }

    private async Task HandleEditClick(MouseEventArgs _, RoleResult context)
    {
        throw new NotImplementedException();
    }

    private async Task HandleDeleteClick(MouseEventArgs _, RoleResult context)
    {
        throw new NotImplementedException();
    }
}