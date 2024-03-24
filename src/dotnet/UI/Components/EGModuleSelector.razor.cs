namespace UI.Components;

using EncyclopediaGalactica.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Components;

public partial class EGModuleSelector
{
    private List<ModuleResult> _moduleResults = new List<ModuleResult>
    {
        new ModuleResult { Id = 0, Name = "Administration", Description = "Administration" },
        new ModuleResult { Id = 1, Name = "Documents", Description = "Documents" },
        new ModuleResult { Id = 2, Name = "Finance", Description = "Finance" },
        new ModuleResult { Id = 3, Name = "StarMap", Description = "StarMap" },
    };

    private ModuleResult? SelectedModule;

    private EventCallback SelectedOptionChanged()
    {
        throw new NotImplementedException();
    }
}