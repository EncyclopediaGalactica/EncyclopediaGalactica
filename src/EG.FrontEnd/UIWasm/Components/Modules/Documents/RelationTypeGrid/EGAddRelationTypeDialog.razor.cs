using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

namespace UIWasm.Components.Modules.Documents.RelationTypeGrid;

public partial class EGAddRelationTypeDialog
{
    private DocumentResult? SelectedLeftDocument;
    private ApplicationResult? SelectedApplication;
    private DocumentResult? SelectedRightDocument;
    private IEnumerable<DocumentResult>? _documents = new List<DocumentResult>();
    private IEnumerable<ApplicationResult>? _applications = new List<ApplicationResult>();

    [CascadingParameter]
    public IDialogService DialogService { get; set; }

    [Inject]
    private ILogger<EGAddRelationTypeDialog> Logger { get; set; }

    [Inject]
    private IDocumentStructureService DocumentStructureService { get; set; }

    [Inject]
    private IApplicationService ApplicationService { get; set; }

    [Parameter]
    public RelationTypeResult Content { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _documents = await DocumentStructureService.GetAllAsync().ConfigureAwait(false);
        _applications = await ApplicationService.GetAllAsync().ConfigureAwait(false);
    }
}