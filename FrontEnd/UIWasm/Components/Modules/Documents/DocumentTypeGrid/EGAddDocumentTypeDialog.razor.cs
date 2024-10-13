using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Components.Modules.Documents.ApplicationGrid;

namespace UIWasm.Components.Modules.Documents.DocumentTypeGrid;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGAddDocumentTypeDialog
{
    [Inject]
    public ILogger<EGAddApplicationDialog> Logger { get; set; }

    [CascadingParameter]
    public FluentDialog? DialogService { get; set; }

    [Parameter]
    public DocumentTypeResult Content { get; set; }
}