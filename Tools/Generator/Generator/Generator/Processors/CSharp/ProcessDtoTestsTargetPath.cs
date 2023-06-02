namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using System.Text;
using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessDtoTestsTargetPath(List<TypeInfo> typeInfos)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available typeinfo");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (string.IsNullOrEmpty(typeInfo.OriginalTargetDirectoryToken) ||
                string.IsNullOrWhiteSpace(typeInfo.OriginalTargetDirectoryToken))
            {
                continue;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(_pathManager.CheckIfPathAbsoluteOrMakeItOne(typeInfo.OriginalTargetDirectoryToken));

            if (!string.IsNullOrEmpty(typeInfo.OriginalDtoTestProjectBasePathToken)
                && !string.IsNullOrWhiteSpace(typeInfo.OriginalDtoTestProjectBasePathToken))
            {
                builder
                    .Append("/")
                    .Append(_stringManager.CheckIfLastCharSlashAndRemoveIt(
                        typeInfo.OriginalDtoTestProjectBasePathToken));
            }

            if (!string.IsNullOrEmpty(typeInfo.OriginalDtoTestProjectAdditionalPathToken)
                && !string.IsNullOrWhiteSpace(typeInfo.OriginalDtoTestProjectAdditionalPathToken))
            {
                builder
                    .Append("/")
                    .Append(_stringManager.CheckIfLastCharSlashAndRemoveIt(
                        typeInfo.OriginalDtoTestProjectAdditionalPathToken));
            }

            typeInfo.AbsoluteTargetPath = builder.ToString();
        }
        
    }
}