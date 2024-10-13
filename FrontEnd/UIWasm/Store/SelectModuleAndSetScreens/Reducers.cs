namespace UIWasm.Store.SelectModuleAndSetScreens;

using Fluxor;

public static class Reducers
{
    [ReducerMethod]
    public static ModuleAndScreenState ReduceModuleIsSelectedAction(
        ModuleAndScreenState moduleAndScreenState,
        ModuleIsSelectedAction action)
        => new(action.SelectedModule,
            null,
            null,
            true);

    [ReducerMethod]
    public static ModuleAndScreenState ReduceScreensForModuleFetchedAction(
        ModuleAndScreenState moduleAndScreenState,
        ScreensForModuleFetchedResultAction resultAction) =>
        new(moduleAndScreenState.SelectedModule,
            resultAction.ScreensOfAModule,
            null,
            false);

    [ReducerMethod]
    public static ModuleAndScreenState ReduceScreenIsSelectedAction(
        ModuleAndScreenState moduleAndScreenState,
        ScreenIsSelectedAction action) =>
        new(moduleAndScreenState.SelectedModule,
            moduleAndScreenState.ScreensOfModule,
            action.SelectedScreen,
            moduleAndScreenState.IsLoading);
}