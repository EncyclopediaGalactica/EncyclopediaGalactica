namespace UI.States;

using Fluxor;

[FeatureState]
public class ModuleAndScreenState
{
    private ModuleAndScreenState()
    {
    }

    public ModuleAndScreenState(long moduleId)
    {
        ModuleId = moduleId;
    }

    public long ModuleId { get; }
    public long ScreenId { get; }
}