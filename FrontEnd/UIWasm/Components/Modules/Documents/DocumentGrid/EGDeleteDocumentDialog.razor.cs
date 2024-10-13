using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace UIWasm.Components.Modules.Documents.DocumentGrid;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGDeleteDocumentDialog
{
    [Parameter]
    public DocumentResult Content { get; set; }

    [CascadingParameter]
    public FluentDialog? DialogService { get; set; }
}