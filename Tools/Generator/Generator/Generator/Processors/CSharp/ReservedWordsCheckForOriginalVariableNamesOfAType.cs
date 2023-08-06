namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ReservedWordsCheckForOriginalVariableNamesOfAType(List<TypeInfo> typeInfos, List<string> reservedWords)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        if (!reservedWords.Any())
        {
            _logger.LogInformation("Reserved words list is empty");
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            foreach (VariableInfo variableInfo in fileInfo.VariableInfos)
            {
                if (string.IsNullOrEmpty(variableInfo.OriginalVariableNameToken)
                    || string.IsNullOrWhiteSpace(variableInfo.OriginalVariableNameToken))
                {
                    continue;
                }

                if (reservedWords.Contains(variableInfo.OriginalVariableNameToken.Trim().ToLower()))
                {
                    throw new GeneratorException(
                        $"{variableInfo.OriginalVariableNameToken} is a reserved word.");
                }
            }
        }
    }
}