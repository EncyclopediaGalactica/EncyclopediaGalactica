namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Managers.SolutionProjectManager;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void SetupSolutionProjectManagers(SolutionInfo solutionInfo)
    {
        solutionInfo.ProjectInfos.ForEach(item =>
        {
            item.SolutionProjectManager = new SolutionProjectManager.Builder()
                .SetSolutionProjectFilePath(item.ProjectFileWithFullPath)
                .SetSlotName(item.Slot)
                .Build();
        });
    }
}