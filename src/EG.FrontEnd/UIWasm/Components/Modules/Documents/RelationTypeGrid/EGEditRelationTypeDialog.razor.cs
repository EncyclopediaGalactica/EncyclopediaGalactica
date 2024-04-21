using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace UIWasm.Components.Modules.Documents.RelationTypeGrid;

public partial class EGEditRelationTypeDialog
{
    [CascadingParameter]
    public FluentDialog? Dialog { get; set; }

    [Inject]
    private ILogger<EGEditRelationTypeDialog> Logger { get; set; }

    [Parameter]
    public RelationTypeResult Content { get; set; }

    protected override Task OnInitializedAsync()
    {
        Logger.LogInformation("loaded");
        return base.OnInitializedAsync();
    }
}