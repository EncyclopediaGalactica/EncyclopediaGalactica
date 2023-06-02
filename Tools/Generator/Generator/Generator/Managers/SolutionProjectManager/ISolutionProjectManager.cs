namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.SolutionProjectManager;

using Models;

public interface ISolutionProjectManager
{
    /// <summary>
    ///     Checks if given project based on slot name is already added to the projects or not. If not it will add it.
    /// </summary>
    /// <param name="slots">List of slot names</param>
    /// <param name="solutionInfo">The <see cref="SolutionInfo" /> instance</param>
    void CheckSolutionProjectReferenceOrAdd(List<string> slots, SolutionInfo solutionInfo);
}