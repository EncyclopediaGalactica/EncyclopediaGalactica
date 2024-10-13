namespace UIWasm.Components.Modules.Administration.UsersScreen;

using EncyclopediaGalactica.Common.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

public partial class EGEditUserDialog
{
    [Inject]
    private ILogger<EGEditUserDialog> Logger { get; set; }

    [CascadingParameter]
    public FluentDialog? Dialog { get; set; }

    [Parameter]
    public UserResult Content { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("Dialog");
        await base.OnInitializedAsync();
    }
}