namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessDtoTargetPath(SolutionInfo solutionInfo, List<TypeInfo> typeInfos)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        // solutionInfo.ProjectInfos.ForEach(projectInfo => { projectInfo. });
    }
}