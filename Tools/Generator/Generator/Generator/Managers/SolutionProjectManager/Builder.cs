namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.SolutionProjectManager;

using Microsoft.Build.Construction;

public partial class SolutionProjectManager
{
    public class Builder
    {
        private string _slotName;
        private string _solutionProjectFilePath;

        public Builder SetSolutionProjectFilePath(string path)
        {
            _solutionProjectFilePath = path;
            return this;
        }

        public Builder SetSlotName(string slotName)
        {
            _slotName = slotName;
            return this;
        }

        public SolutionProjectManager Build()
        {
            ProjectRootElement? rootElement = ProjectRootElement.Open(_solutionProjectFilePath);
            if (rootElement is null)
            {
                throw new GeneratorException($"{_solutionProjectFilePath} cannot be opened");
            }

            return new SolutionProjectManager(rootElement, _slotName);
        }
    }
}