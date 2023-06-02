namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using System.Text;
using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessDtoTargetPath(List<TypeInfo> typeInfos)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(fileInfo.OriginalTargetDirectoryToken)
                && !string.IsNullOrWhiteSpace(fileInfo.OriginalTargetDirectoryToken))
            {
                builder.Append(_pathManager.CheckIfPathAbsoluteOrMakeItOne(fileInfo.OriginalTargetDirectoryToken));
            }

            if (!string.IsNullOrEmpty(fileInfo.OriginalDtoProjectBasePathToken)
                && !string.IsNullOrWhiteSpace(fileInfo.OriginalDtoProjectBasePathToken))
            {
                _stringManager.CheckIfFirstCharIsSlashAndThrow(fileInfo.OriginalDtoProjectBasePathToken);
                builder
                    .Append("/")
                    .Append(_stringManager.CheckIfLastCharSlashAndRemoveIt(fileInfo.OriginalDtoProjectBasePathToken));
            }

            if (!string.IsNullOrEmpty(fileInfo.OriginalDtoProjectAdditionalPathToken)
                && !string.IsNullOrWhiteSpace(fileInfo.OriginalDtoProjectAdditionalPathToken))
            {
                _stringManager.CheckIfFirstCharIsSlashAndThrow(fileInfo.OriginalDtoProjectAdditionalPathToken);
                builder
                    .Append("/")
                    .Append(_stringManager.CheckIfLastCharSlashAndRemoveIt(
                        fileInfo.OriginalDtoProjectAdditionalPathToken));
            }

            fileInfo.AbsoluteTargetPath = builder.ToString();
        }
    }
}