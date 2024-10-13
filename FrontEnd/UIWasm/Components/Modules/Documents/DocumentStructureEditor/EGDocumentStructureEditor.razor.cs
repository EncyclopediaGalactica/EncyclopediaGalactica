#region

using Microsoft.AspNetCore.Components;
using UIWasm.Services;

#endregion

namespace UIWasm.Components.Modules.Documents.DocumentStructureEditor;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGDocumentStructureEditor
{
    private ICollection<DocumentResult> _documentResults = new List<DocumentResult>();
    private DocumentResult? _selectedDocument;
    private StructureNode? _structureNode;

    [Inject]
    private IDocumentService DocumentService { get; set; }

    [Inject]
    private IDocumentStructureService DocumentStructureService { get; set; }

    [Inject]
    private ILogger<EGDocumentStructureEditor> Logger { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _documentResults = await DocumentService.GetAllAsync().ConfigureAwait(false);
        Logger.LogInformation("documents size: {Size}", _documentResults.Count);
    }

    private async Task SelectDocumentAsync(DocumentResult selectedDocument)
    {
        _selectedDocument = await DocumentService.GetById(selectedDocument.Id).ConfigureAwait(false);
        // List<StructureNode> flatNodes = MapStructureNodeResultToStructureNode(_selectedDocument.StructureNodes);
        _structureNode = new StructureNode
        {
            Id = 1,
            DocumentId = 1,
            StructureNodes = new List<StructureNode>
            {
                new StructureNode
                {
                    Id = 2,
                    DocumentId = 1,
                    StructureNodes = new List<StructureNode>
                    {
                        new StructureNode
                        {
                            Id = 21,
                            DocumentId = 1,
                            StructureNodes = new List<StructureNode>
                            {
                                new StructureNode
                                {
                                    Id = 211,
                                    DocumentId = 1,
                                    StructureNodes = new List<StructureNode>
                                    {
                                    }
                                },
                                new StructureNode
                                {
                                    Id = 212,
                                    DocumentId = 1,
                                    StructureNodes = new List<StructureNode>
                                    {
                                    }
                                },
                                new StructureNode
                                {
                                    Id = 213,
                                    DocumentId = 1,
                                    StructureNodes = new List<StructureNode>
                                    {
                                    }
                                },
                            }
                        },
                        new StructureNode
                        {
                            Id = 3,
                            DocumentId = 1,
                            StructureNodes = new List<StructureNode>
                            {
                                new StructureNode
                                {
                                    Id = 31,
                                    DocumentId = 1,
                                    StructureNodes = new List<StructureNode>
                                    {
                                        new StructureNode
                                        {
                                            Id = 311,
                                            DocumentId = 1,
                                            StructureNodes = new List<StructureNode>
                                            {
                                            }
                                        },
                                        new StructureNode
                                        {
                                            Id = 312,
                                            DocumentId = 1,
                                            StructureNodes = new List<StructureNode>
                                            {
                                            }
                                        },
                                        new StructureNode
                                        {
                                            Id = 313,
                                            DocumentId = 1,
                                            StructureNodes = new List<StructureNode>
                                            {
                                            }
                                        },
                                    }
                                },
                                new StructureNode
                                {
                                    Id = 4,
                                    DocumentId = 1,
                                    StructureNodes = new List<StructureNode>
                                    {
                                        new StructureNode
                                        {
                                            Id = 41,
                                            DocumentId = 1,
                                            StructureNodes = new List<StructureNode>
                                            {
                                                new StructureNode
                                                {
                                                    Id = 411,
                                                    DocumentId = 1,
                                                    StructureNodes = new List<StructureNode>
                                                    {
                                                    }
                                                },
                                                new StructureNode
                                                {
                                                    Id = 412,
                                                    DocumentId = 1,
                                                    StructureNodes = new List<StructureNode>
                                                    {
                                                    }
                                                },
                                                new StructureNode
                                                {
                                                    Id = 413,
                                                    DocumentId = 1,
                                                    StructureNodes = new List<StructureNode>
                                                    {
                                                    }
                                                },
                                            }
                                        },
                                    }
                                },
                            }
                        }
                    }
                }
            }
        };
    }
}