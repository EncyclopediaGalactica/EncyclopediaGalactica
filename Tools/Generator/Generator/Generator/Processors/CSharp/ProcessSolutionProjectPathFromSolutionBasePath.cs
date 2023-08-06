namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using System.Text;
using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionProjectPathFromSolutionBasePath(SolutionInfo solutionInfo)
    {
        if (!solutionInfo.ProjectInfos.Any())
        {
            _logger.LogInformation("{Input} is empty", nameof(solutionInfo.ProjectInfos));
            return;
        }

        solutionInfo.ProjectInfos.ForEach(item =>
        {
            item.PathFromSolutionBase = new StringBuilder()
                .Append("..\\")
                .Append(item.Name)
                .Append("\\")
                .Append(item.Name)
                .Append(".csproj")
                .ToString();
        });
    }
}