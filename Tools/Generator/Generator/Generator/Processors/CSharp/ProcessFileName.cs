namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessFileName(List<TypeInfo> typeInfos, string fileNamePostFix, string fileType)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
        }

        if (string.IsNullOrEmpty(fileNamePostFix)
            || string.IsNullOrWhiteSpace(fileNamePostFix))
        {
            _logger.LogInformation("Typename postfix is empty, null or whitespace");
        }

        if (string.IsNullOrEmpty(fileType)
            || string.IsNullOrWhiteSpace(fileType))
        {
            throw new ArgumentException("No file type provided");
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            string fileName = _stringManager.Concat(
                _stringManager.MakeFirstCharUpperCase(fileInfo.OriginalTypeNameToken),
                _stringManager.MakeFirstCharUpperCase(fileNamePostFix),
                _stringManager.CheckIfFirstCharIsDotOrAddIt(
                    _stringManager.ToLowerCase(fileType))
            );
            fileInfo.FileName = fileName;
        }
    }
}