using Microsoft.AspNetCore.Components;

namespace UIWasm.Components.Modules.Documents.Document.DocumentTypeGrid;

public partial class EGDocumentTypeGrid
{
    [Inject]
    private ILogger<EGDocumentTypeGrid> Logger { get; set; }

    [Inject]
    private IDocumentTypeService DocumentTypeService { get; set; }
}