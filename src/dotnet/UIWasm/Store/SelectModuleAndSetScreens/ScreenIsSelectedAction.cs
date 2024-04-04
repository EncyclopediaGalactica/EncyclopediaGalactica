namespace UIWasm.Store.SelectModuleAndSetScreens;

public class ScreenIsSelectedAction
{
    private ScreenIsSelectedAction()
    {
    }

    public ScreenIsSelectedAction(string selectedScreen)
    {
        SelectedScreen = selectedScreen;
    }

    public string SelectedScreen { get; }
}