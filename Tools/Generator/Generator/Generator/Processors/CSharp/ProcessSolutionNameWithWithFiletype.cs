namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionNameWithWithFileType(SolutionInfo solutionInfo)
    {
        solutionInfo.SolutionNameWithFileExtension = _stringManager.ConcatForCSharpProjectName(
            solutionInfo.Name,
            solutionInfo.SolutionFileType);
    }
}