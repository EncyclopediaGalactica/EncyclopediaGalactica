namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessNullableVariableTypes(List<TypeInfo> typeInfos)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            foreach (VariableInfo propertyInfo in fileInfo.VariableInfos)
            {
                if (string.IsNullOrEmpty(propertyInfo.OriginalVariableNameToken)
                    || string.IsNullOrWhiteSpace(propertyInfo.OriginalVariableNameToken))
                {
                    continue;
                }

                if (fileInfo.RequiredProperties is not null
                    && fileInfo.RequiredProperties.Any()
                    && fileInfo.RequiredProperties.Contains(propertyInfo.OriginalVariableNameToken.ToLower()))
                {
                    propertyInfo.IsNullable = false;
                    continue;
                }

                propertyInfo.IsNullable = true;
            }
        }
    }
}