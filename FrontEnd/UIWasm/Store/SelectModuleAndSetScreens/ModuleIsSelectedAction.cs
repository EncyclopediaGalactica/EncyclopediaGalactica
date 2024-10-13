namespace UIWasm.Store.SelectModuleAndSetScreens;

using EncyclopediaGalactica.Common.Contracts;

public class ModuleIsSelectedAction(ModuleResult selectedModule)
{
    public ModuleResult SelectedModule { get; } = selectedModule;
}