using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace UIWasm.Components.Modules.Documents.DocumentStructuresGrid;

public partial class EGAddDocumentStructureDialog
{
    [CascadingParameter]
    public FluentDialog? DialogService { get; set; }

    [Parameter]
    public DocumentStructureResult Content { get; set; }
}