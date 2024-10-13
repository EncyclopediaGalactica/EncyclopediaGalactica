#region

using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

#endregion

namespace UIWasm.Components.Modules.Documents.RelationGrid;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGEditRelationDialog
{
    [CascadingParameter]
    public FluentDialog? FluentDialog { get; set; }

    [Parameter]
    public RelationResult Content { get; set; }
}