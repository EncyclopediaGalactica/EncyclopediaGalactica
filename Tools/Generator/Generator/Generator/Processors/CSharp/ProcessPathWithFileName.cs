namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessPathWithFileName(List<TypeInfo> typeInfos)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            if (string.IsNullOrEmpty(fileInfo.AbsoluteTargetPath) ||
                string.IsNullOrWhiteSpace(fileInfo.AbsoluteTargetPath))
            {
                _logger.LogInformation("Target directory is not defined");
                return;
            }

            if (string.IsNullOrEmpty(fileInfo.FileName) || string.IsNullOrWhiteSpace(fileInfo.FileName))
            {
                _logger.LogInformation("Filename is not defined");
                return;
            }

            fileInfo.TargetPathWithFileName = _pathManager.BuildPathString(
                fileInfo.AbsoluteTargetPath,
                fileInfo.FileName);
        }
    }
}