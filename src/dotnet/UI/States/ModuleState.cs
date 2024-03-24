namespace UI.States;

using Fluxor;

[FeatureState]
public class ModuleState
{
    private ModuleState()
    {
    }

    public ModuleState(long moduleId)
    {
        ModuleId = moduleId;
    }

    public long ModuleId { get; }
}