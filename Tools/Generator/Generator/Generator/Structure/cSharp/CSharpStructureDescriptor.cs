namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Structure.cSharp;

using Microsoft.Extensions.Logging;

public partial class CSharpStructureDescriptor : IStructureDescriptor
{
    private readonly Dictionary<string, ProjectDescriptor> _structure = new Dictionary<string, ProjectDescriptor>
    {
        {
            Slots.DtoProject,
            new ProjectDescriptor
            {
                NamingPattern = "Dto",
                DependenciesBySlotName = new List<string>()
            }
        },
        {
            Slots.DtoProjectTestUnit,
            new ProjectDescriptor
            {
                NamingPattern = "Dto.Test.Unit",
                DependenciesBySlotName = new List<string>
                {
                    Slots.DtoProject
                }
            }
        }
    };

    private Logger<CSharpStructureDescriptor> _logger = new Logger<CSharpStructureDescriptor>(
        LoggerFactory.Create(o => o.AddConsole()));

    private struct Slots
    {
        public const string DtoProject = "dto_project";
        public const string DtoProjectTestUnit = "dto_project_test_unit";
    }
}