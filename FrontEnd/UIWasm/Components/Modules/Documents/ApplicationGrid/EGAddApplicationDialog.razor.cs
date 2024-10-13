using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace UIWasm.Components.Modules.Documents.ApplicationGrid;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGAddApplicationDialog
{
    [CascadingParameter]
    public IDialogService DialogService { get; set; }

    [Parameter]
    public ApplicationResult Content { get; set; }
}