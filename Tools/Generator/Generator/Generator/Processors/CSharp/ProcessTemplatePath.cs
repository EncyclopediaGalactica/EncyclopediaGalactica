namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessTemplatePath(List<TypeInfo> typeInfos, string templatePath)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
        }

        if (string.IsNullOrEmpty(templatePath) || string.IsNullOrWhiteSpace(templatePath))
        {
            _logger.LogInformation("Dto template path is not provided");
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            templatePath = _pathManager.CheckIfPathAbsoluteOrMakeItOne(templatePath);

            fileInfo.TemplateAbsolutePathWithFileName = templatePath;
        }
    }
}