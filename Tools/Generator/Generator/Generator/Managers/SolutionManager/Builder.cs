namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.SolutionManager;

using Microsoft.Build.Construction;
using Microsoft.Extensions.Logging;
using Models;

public partial class SolutionManager : ISolutionManager
{
    private Logger<SolutionManager> _logger = new Logger<SolutionManager>(
        LoggerFactory.Create(o => o.AddConsole()));

    private SolutionManager(
        SolutionFile solutionFile)
    {
        SolutionFile = solutionFile;
    }

    private SolutionFile SolutionFile { get; init; }
    private Dictionary<string, ProjectRootElement> SolutionProjects { get; init; }

    public class Builder
    {
        private SolutionInfo _solutionInfo;

        public Builder SolutionInfo(SolutionInfo solutionInfo)
        {
            _solutionInfo = solutionInfo;
            return this;
        }

        public SolutionManager Build()
        {
            Microsoft.Build.Construction.SolutionFile solutionFile = SolutionFile.Parse(
                _solutionInfo.SolutionFileWithFullPath);

            return new SolutionManager(solutionFile);
        }
    }
}