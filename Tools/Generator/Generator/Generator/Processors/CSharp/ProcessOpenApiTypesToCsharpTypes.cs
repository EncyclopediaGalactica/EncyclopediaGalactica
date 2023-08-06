namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessOpenApiTypesToCsharpTypes(
        List<TypeInfo> typeInfos,
        Dictionary<string, string> openApiCsharpTypeMap)
    {
        if (typeInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            foreach (VariableInfo variableInfo in fileInfo.VariableInfos)
            {
                variableInfo.VariableTypeName = DecideCsharpType(
                    variableInfo.OriginalVariableTypeNameToken,
                    variableInfo.OriginalVariableTypeFormat,
                    openApiCsharpTypeMap);
                SetUpIsTypeSwitches(variableInfo);
            }
        }
    }

    private void SetUpIsTypeSwitches(VariableInfo variableInfo)
    {
        if (variableInfo.VariableTypeName == "string")
        {
            variableInfo.IsString = true;
        }

        if (variableInfo.VariableTypeName == "int")
        {
            variableInfo.IsInt = true;
        }

        if (variableInfo.VariableTypeName == "long")
        {
            variableInfo.IsLong = true;
        }

        if (variableInfo.VariableTypeName == "float")
        {
            variableInfo.IsFloat = true;
        }

        if (variableInfo.VariableTypeName == "double")
        {
            variableInfo.IsDouble = true;
        }

        if (variableInfo.VariableTypeName == "bool")
        {
            variableInfo.IsBool = true;
        }
    }

    private string DecideCsharpType(
        string originalPropertyTypenameToken,
        string? originalPropertyTypeFormatToken,
        Dictionary<string, string> openApiCsharpTypeMap)
    {
        if (string.IsNullOrEmpty(originalPropertyTypenameToken)
            || string.IsNullOrWhiteSpace(originalPropertyTypenameToken))
        {
            _logger.LogInformation("Original property type name parameter is empty. Exiting.");
            return string.Empty;
        }

        if (!openApiCsharpTypeMap.Any())
        {
            _logger.LogInformation("Open api type - csharp type map is empty. Exiting.");
            return string.Empty;
        }

        string type;
        switch (originalPropertyTypenameToken.ToLower())
        {
            case "integer":

                if (string.IsNullOrEmpty(originalPropertyTypeFormatToken)
                    || string.IsNullOrWhiteSpace(originalPropertyTypeFormatToken))
                {
                    throw new GeneratorException($"No format is specified for integer");
                }

                switch (originalPropertyTypeFormatToken.ToLower())
                {
                    case "int32":
                        openApiCsharpTypeMap.TryGetValue("integer-int32", out type);
                        return type;

                    case "int64":
                        openApiCsharpTypeMap.TryGetValue("integer-int64", out type);
                        return type;

                    default:
                        throw new GeneratorException(
                            $"No type for {originalPropertyTypenameToken} - {originalPropertyTypeFormatToken}");
                }

            case "number":

                if (string.IsNullOrEmpty(originalPropertyTypeFormatToken)
                    || string.IsNullOrWhiteSpace(originalPropertyTypeFormatToken))
                {
                    throw new GeneratorException($"No format is specified for number");
                }

                switch (originalPropertyTypeFormatToken.ToLower())
                {
                    case "float":
                        openApiCsharpTypeMap.TryGetValue("number-float", out type);
                        return type;

                    case "double":
                        openApiCsharpTypeMap.TryGetValue("number-double", out type);
                        return type;

                    default:
                        throw new GeneratorException(
                            $"No type for {originalPropertyTypenameToken} - {originalPropertyTypeFormatToken}");
                }

            case "string":
                if (!string.IsNullOrEmpty(originalPropertyTypeFormatToken)
                    || !string.IsNullOrWhiteSpace(originalPropertyTypeFormatToken))
                {
                    switch (originalPropertyTypeFormatToken.ToLower())
                    {
                        case "byte":
                            openApiCsharpTypeMap.TryGetValue("string-byte", out type);
                            return type;
                        case "binary":
                            openApiCsharpTypeMap.TryGetValue("string-binary", out type);
                            return type;
                        case "date":
                            openApiCsharpTypeMap.TryGetValue("string-date", out type);
                            return type;
                        case "date-time":
                            openApiCsharpTypeMap.TryGetValue("string-date-time", out type);
                            return type;
                        default:
                            openApiCsharpTypeMap.TryGetValue("string", out type);
                            return type;
                    }
                }

                openApiCsharpTypeMap.TryGetValue("string", out type);
                return type;

            case "boolean":
                openApiCsharpTypeMap.TryGetValue("boolean", out type);
                return type;
            default:
                throw new GeneratorException();
        }
    }
}