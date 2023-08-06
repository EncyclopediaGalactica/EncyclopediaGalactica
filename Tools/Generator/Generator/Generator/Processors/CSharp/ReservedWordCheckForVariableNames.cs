namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    public void ReservedWordCheckForVariableNames(
        List<TypeInfo> typeInfos,
        List<string> reservedWords)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            foreach (VariableInfo variableInfo in fileInfo.VariableInfos)
            {
                if (string.IsNullOrEmpty(variableInfo.VariableName)
                    || string.IsNullOrWhiteSpace(variableInfo.VariableName))
                {
                    _logger.LogInformation("{VarName} variable does not have name. Probably variable names are " +
                                           "not processed yet", variableInfo.OriginalVariableNameToken);
                    continue;
                }

                if (reservedWords.Contains(variableInfo.VariableName))
                {
                    _logger.LogInformation(
                        "Property name - {PN} - is a reserved word. Please fix it",
                        variableInfo.OriginalVariableTypeNameToken);
                }
            }
        }
    }
}