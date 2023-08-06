namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionName(SolutionInfo solutionInfo)
    {
        string solutionName = _stringManager.MakeFirstCharUpperCase(
            solutionInfo.OriginalNameToken);
        solutionInfo.Name = solutionName;
    }
}