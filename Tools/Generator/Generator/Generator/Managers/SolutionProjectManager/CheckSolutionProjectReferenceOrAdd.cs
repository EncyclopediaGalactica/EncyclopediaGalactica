namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.SolutionProjectManager;

using Microsoft.Build.Construction;
using Microsoft.Extensions.Logging;
using Models;

public partial class SolutionProjectManager
{
    /// <inheritdoc />
    public void CheckSolutionProjectReferenceOrAdd(
        List<string> slots,
        SolutionInfo solutionInfo)
    {
        if (!slots.Any())
        {
            _logger.LogInformation("{Slot} got zero length list for referencing projects", _slotName);
            return;
        }

        foreach (string slot in slots)
        {
            string referencedProjectPathFromSolutionBasePath = solutionInfo.ProjectInfos
                .Find(p => p.Slot == slot).PathFromSolutionBase;
            List<ProjectItemGroupElement> result = _rootElement.ItemGroups.Where(
                p => p.Items.Count(i => i.ElementName.ToLower().Equals("ProjectReference".ToLower())) > 0
            ).ToList();

            ProjectItemGroupElement? projectReference = result
                .Find(p => p.Items.Any(
                    i => i.ElementName.ToLower().Equals("ProjectReference")
                         && i.Include.ToLower().Equals(referencedProjectPathFromSolutionBasePath)));

            if (projectReference is not null)
            {
                continue;
            }
        }
    }
}