namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessTestTypeName(List<TypeInfo> typeInfos, string? testTypeNamePostfix = default)
    {
        ProcessTypeName(typeInfos, testTypeNamePostfix);
    }
}