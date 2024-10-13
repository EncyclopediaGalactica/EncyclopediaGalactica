using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace UIWasm.Components.Modules.Documents.FileFormatGrid;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGDeleteFileFormatDialog
{
    [Parameter]
    public FiletypeResult Content { get; set; }

    [CascadingParameter]
    public FluentDialog? FluentDialog { get; set; }
}