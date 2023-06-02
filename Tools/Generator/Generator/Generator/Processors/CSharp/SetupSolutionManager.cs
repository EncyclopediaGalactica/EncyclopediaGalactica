namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Managers.SolutionManager;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void SetupSolutionManager(SolutionInfo solutionInfo)
    {
        solutionInfo.SolutionManager = new SolutionManager.Builder()
            .SolutionInfo(solutionInfo)
            .Build();
    }
}