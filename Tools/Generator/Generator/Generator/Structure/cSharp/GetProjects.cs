namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Structure.cSharp;

public partial class CSharpStructureDescriptor
{
    /// <inheritdoc />
    public List<string> GetProjects()
    {
        return _structure.Keys.ToList();
    }
}