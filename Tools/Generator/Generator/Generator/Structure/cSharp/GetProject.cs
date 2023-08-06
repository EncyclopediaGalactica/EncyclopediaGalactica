namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Structure.cSharp;

public partial class CSharpStructureDescriptor
{
    /// <inheritdoc />
    public ProjectDescriptor GetProject(string itemSlot)
    {
        return _structure[itemSlot];
    }
}