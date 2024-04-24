using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace UIWasm.Components.Modules.Documents.FileFormatGrid;

public partial class EGEditFileFormatDialog
{
    [Parameter]
    public FileFormatResult Content { get; set; }

    [CascadingParameter]
    public FluentDialog? FluentDialog { get; set; }
}
