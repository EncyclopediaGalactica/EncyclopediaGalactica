namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void AddImportsToTestTypes(List<TypeInfo> dtoTestTypeInfos, List<TypeInfo> dtoTypeInfos)
    {
        if (!dtoTestTypeInfos.Any() || !dtoTypeInfos.Any())
        {
            _logger.LogInformation("Either Dto Test typeInfo list " +
                                   "or Dto Typeinfo list is empty. Exiting");
            return;
        }

        foreach (TypeInfo dtoTypeInfo in dtoTypeInfos)
        {
            TypeInfo? dtoTestTypeInfo = dtoTestTypeInfos
                .FirstOrDefault(p => p.OriginalTypeNameToken == dtoTypeInfo.OriginalTypeNameToken);

            if (dtoTestTypeInfo is null)
            {
                _logger.LogInformation("There is no Dto Test type for {Type}. Skipping",
                    dtoTypeInfo.OriginalTypeNameToken);
                continue;
            }

            if (string.IsNullOrEmpty(dtoTypeInfo.Namespace) || string.IsNullOrWhiteSpace(dtoTypeInfo.Namespace))
            {
                _logger.LogInformation("Either namespace or typename value for {Type} is null or empty. " +
                                       "Probably the method is called before these are processed. " +
                                       "Original type name token is provided for debugging",
                    dtoTypeInfo.OriginalTypeNameToken);
                continue;
            }

            dtoTestTypeInfo.Imports.Add(dtoTypeInfo.Namespace);
        }
    }
}