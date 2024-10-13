#region

using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

#endregion

namespace UIWasm.Components.Modules.Documents.DocumentStructuresGrid;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGEditDocumentStructureDialog
{
    [CascadingParameter]
    public FluentDialog? DialogService { get; set; }

    [Parameter]
    public DocumentStructureResult Content { get; set; }
}