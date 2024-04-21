using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace UIWasm.Components.Modules.Documents.DocumentGrid;

public partial class EGPreviewDocumentDialog
{
    [Parameter]
    public DocumentResult Content { get; set; }

    [CascadingParameter]
    public FluentDialog? DialogService { get; set; }
}