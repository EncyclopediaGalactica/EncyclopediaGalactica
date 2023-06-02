namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Managers.SolutionProjectManager;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void CheckSolutionProjectDependencies(SolutionInfo solutionInfo)
    {
        solutionInfo.ProjectInfos.ForEach(item =>
        {
            List<string> projectstoBeReferenced = _structureDescriptor.GetProject(item.Slot).DependenciesBySlotName;
            SolutionProjectManager solutionProjectManager = item.SolutionProjectManager;
            projectstoBeReferenced.ForEach(projects =>
            {
                solutionProjectManager.CheckSolutionProjectReferenceOrAdd(projectstoBeReferenced, solutionInfo);
            });
        });
    }
}