namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionProjectNames(SolutionInfo solutionInfo)
    {
        if (string.IsNullOrEmpty(solutionInfo.Name) || string.IsNullOrWhiteSpace(solutionInfo.Name))
        {
            _logger.LogInformation("{Name} is empty or null", solutionInfo.Name);
            return;
        }

        solutionInfo.ProjectInfos.ForEach(item =>
        {
            string pattern = _structureDescriptor.GetNamingPatternBySlot(item.Slot);
            item.Name = _stringManager.ConcatForCSharpProjectName(
                solutionInfo.Name,
                pattern);
        });
    }
}