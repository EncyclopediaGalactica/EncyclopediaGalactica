namespace UIWasm.Store.SelectModuleAndSetScreens;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public class ModuleIsSelectedAction(ModuleResult selectedModule)
{
    public ModuleResult SelectedModule { get; } = selectedModule;
}