namespace UIWasm.Store.SelectModuleAndSetScreens;

using Fluxor;

public static class Reducers
{
    [ReducerMethod]
    public static ModuleState ReduceModuleIsSelectedAction(ModuleState state, ModuleIsSelectedAction action)
        => new(action.SelectedModule, null, true);

    [ReducerMethod]
    public static ModuleState ReduceScreensForModuleFetchedAction(
        ModuleState state,
        ScreensForModuleFetchedResultAction resultAction) =>
        new(state.SelectedModule, resultAction.ScreensOfAModule, false);
}