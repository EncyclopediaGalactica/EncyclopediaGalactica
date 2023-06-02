namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.SolutionProjectManager;

using Microsoft.Build.Construction;
using Microsoft.Extensions.Logging;

public partial class SolutionProjectManager : ISolutionProjectManager
{
    private Logger<SolutionProjectManager> _logger = new Logger<SolutionProjectManager>(
        LoggerFactory.Create(o => o.AddConsole()));

    private ProjectRootElement _rootElement;
    private string _slotName;

    private SolutionProjectManager(ProjectRootElement rootElement, string slotName)
    {
        ArgumentNullException.ThrowIfNull(rootElement);
        _rootElement = rootElement;
        _slotName = slotName;
    }
}