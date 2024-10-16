using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm.Services;

namespace UIWasm.Components.Modules.Documents.FileFormatGrid;

using EncyclopediaGalactica.Common.Contracts;

public partial class EGFileFormatGrid
{
    [Inject]
    private ILogger<EGFileFormatGrid> Logger { get; set; }

    [Inject]
    private IFiletypeService FiletypeService { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; }

    private FluentDataGrid<FiletypeResult> Grid;
    private GridItemsProvider<FiletypeResult> GridItemsProvider;

    protected async override Task OnInitializedAsync()
    {
        GridItemsProvider = async request =>
        {
            ICollection<FiletypeResult> r = await FiletypeService.GetAllAsync().ConfigureAwait(false);
            return GridItemsProviderResult.From(r, r.Count);
        };
    }

    private async Task HandlePreviewOnClickAsync(MouseEventArgs mouseEventArgs, FiletypeResult context)
    {
        await DialogService.ShowDialogAsync<EGEditFileFormatDialog>(
                context,
                new DialogParameters
                {
                    Width = "600px",
                    Height = "400px",
                    PreventScroll = true,
                    PreventDismissOnOverlayClick = true,
                    PrimaryAction = "Save",
                    PrimaryActionEnabled = true,
                    OnDialogResult = DialogService.CreateDialogCallback(this, HandleEditSaveAsync)
                })
            .ConfigureAwait(false);
    }

    private async Task HandleEditSaveAsync(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("File extension edit is saved");
        }
    }

    private async Task HandleDeleteOnClickAsync(MouseEventArgs mouseEventArgs, FiletypeResult context)
    {
        await DialogService.ShowDialogAsync<EGEditFileFormatDialog>(
                context,
                new DialogParameters
                {
                    Width = "600px",
                    Height = "400px",
                    PreventScroll = true,
                    PreventDismissOnOverlayClick = true,
                    PrimaryAction = "Save",
                    PrimaryActionEnabled = true,
                    OnDialogResult = DialogService.CreateDialogCallback(this, HandleDeleteAsync)
                })
            .ConfigureAwait(false);
    }

    private async Task HandleDeleteAsync(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("File format delete is executed.");
        }
    }

    private async Task HandleAddOnClickAsync()
    {
        await DialogService.ShowDialogAsync<EGAddFileFormatDialog>(
            new FiletypeResult(),
            new DialogParameters
            {
                Width = "600px",
                Height = "400px",
                PreventScroll = true,
                PreventDismissOnOverlayClick = true,
                PrimaryAction = "Save",
                PrimaryActionEnabled = true,
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleAddSaveAsync)
            }).ConfigureAwait(false);
    }

    private async Task HandleAddSaveAsync(DialogResult dialogResult)
    {
        if (dialogResult is { Cancelled: false, Data: not null })
        {
            Logger.LogInformation("New file format is saved.");
        }
    }
}