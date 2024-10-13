#region

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using UIWasm.Services;

#endregion

namespace UIWasm.Components.Modules.Documents.DocumentStructureEditor;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGDocumentStructureTree
{
    private ICollection<DocumentStructureNodeResult> StructureNodes = new List<DocumentStructureNodeResult>();

    [Inject]
    private ILogger<EGDocumentStructureEditor> Logger { get; set; }

    [Parameter]
    public DocumentStructureNodeResult? StructureNode { get; set; }

    [Inject]
    private IDocumentStructureNodeService DocumentStructureNodeService { get; set; }

    protected override void OnInitialized()
    {
        Logger.LogInformation("structure node id: {Id}", StructureNode.Id);
        Logger.LogInformation("has children: {Id}", StructureNode.HasChildren);
        if (StructureNode is not null && StructureNode.HasChildren)
        {
            StructureNodes = DocumentStructureNodeService.GetChildrenOfANode(StructureNode.Id);
        }

        Logger.LogInformation("structurenodes amount: {amount}", StructureNodes.Count);
    }

    private async Task HandleAddStructureNodeAsync(MouseEventArgs mouseEventArgs, long structureNodeId)
    {
        Logger.LogInformation("HandleAddStructureNodeAsync");
    }

    private async Task HandleDeleteStructureNodeAsync(MouseEventArgs mouseEventArgs, long structureNodeId)
    {
        Logger.LogInformation("HandleDeleteStructureNodeAsync");
    }
}