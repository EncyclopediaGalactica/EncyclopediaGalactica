namespace UIWasm.Components.Modules.Administration.UsersScreen;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Services;
using Store.SelectModuleAndSetScreens;

public partial class EGUsersScreen
{
    private FluentDataGrid<UserResult> Grid;
    private GridItemsProvider<UserResult> GridItemsProvider = default!;

    [Inject]
    private IState<ModuleAndScreenState> ModuleAndScreenState { get; set; }

    [Inject]
    private ILogger<EGUsersScreen> Logger { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; }

    [Inject]
    private IUserService UserService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<UserResult> items = await UserService.GetAllAsync();
            return GridItemsProviderResult.From(
                items,
                items.Count
            );
        };
        await base.OnInitializedAsync();
    }

    private async Task HandleEditClickAsync(MouseEventArgs _, UserResult selectedUser)
    {
        await DialogService.ShowDialogAsync<EGEditUserDialog>(selectedUser,
            new DialogParameters
            {
                Height = "400px",
                Title = $"Edit {selectedUser.FirstName} {selectedUser.LastName} details",
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleEditDialog),
                PrimaryAction = "Save",
                PrimaryActionEnabled = true
            });
    }

    private async Task HandleEditDialog(DialogResult dialogResult)
    {
        if (dialogResult.Cancelled)
        {
            Logger.LogInformation("Dialog data: {data}", dialogResult.Cancelled);
            return;
        }

        if (dialogResult.Data is not null)
        {
            UserResult userResult = (UserResult)dialogResult.Data;
            Logger.LogInformation("Dialog data: user name: {name}; first name: {fname}; last name: {lname}",
                userResult.UserName,
                userResult.FirstName,
                userResult.LastName);
            await Grid.RefreshDataAsync();
            return;
        }
    }

    private void HandleDeleteClick(MouseEventArgs _, UserResult selectedUser)
    {
        throw new NotImplementedException();
    }
}