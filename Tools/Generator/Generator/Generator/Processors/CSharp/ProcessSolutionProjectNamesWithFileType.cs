namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionProjectNamesWithFileType(SolutionInfo solutionInfo)
    {
        solutionInfo.ProjectInfos.ForEach(item =>
        {
            item.SolutionProjectFileWithType = _stringManager.ConcatForCSharpProjectName(
                item.Name,
                solutionInfo.SolutionProjectFileType);
        });
    }
}