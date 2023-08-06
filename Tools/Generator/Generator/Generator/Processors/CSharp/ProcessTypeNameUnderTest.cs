namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessTypeNameUnderTest(List<TypeInfo> typeInfos, string dtoTypeNamePostfix)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
        }

        if (typeInfos.Where(p => p.TypeName == null).ToList().Count > 0)
        {
            _logger.LogInformation("TypeNames are not set up for types and as a result defining TypeNames" +
                                   " under tests is not possible. Probably, Process type names should be called " +
                                   "before this method");
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            typeInfo.TypeNameUnderTest = _stringManager.Concat(
                _stringManager.MakeFirstCharUpperCase(typeInfo.OriginalTypeNameToken),
                dtoTypeNamePostfix);
        }
    }
}