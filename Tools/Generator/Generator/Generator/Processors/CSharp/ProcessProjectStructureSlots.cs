namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessProjectStructureSlots(SolutionInfo solutionInfo)
    {
        List<string> projectsInStructure = _structureDescriptor.GetProjects();
        if (!projectsInStructure.Any())
        {
            _logger.LogInformation("Projects in structure is empty");
            return;
        }

        projectsInStructure.ForEach(p => { solutionInfo.ProjectInfos.Add(new ProjectInfo { Slot = p }); });
    }
}