namespace EncyclopediaGalactica.SourceFormats.QA.E2E.BackgroundSteps;

using System;
using Utils.Guards;

public partial class BackgroundSteps
{
    private void AddToContext(string key, object value)
    {
        Guards.StringIsNotNullOrEmptyOrWhitespace(key);
        Guards.IsNotNull(value);
        _scenarioContext.Add(key, value);
    }

    private T GetFromContext<T>(string key)
    {
        Guards.StringIsNotNullOrEmptyOrWhitespace(key);
        return (T)_scenarioContext[key];
    }

    private void OverWriteKeyInContext(string key, object value)
    {
        Guards.StringIsNotNullOrEmptyOrWhitespace(key);
        Guards.IsNotNull(value);
        _scenarioContext.Remove(key);
        _scenarioContext.Add(key, value);
    }

    private struct FuzzyParameters
    {
        public const string EmptyString = "emptystring";
        public const string Null = "null";
        public const string TwoChars = "2chars";
        public const string ThreeSpaces = "3spaces";
    }

    private string? ConvertFuzzyValuesToParameterValue(string parameterValue)
    {
        Guards.StringIsNotNullOrEmptyOrWhitespace(parameterValue);

        switch (parameterValue)
        {
            case FuzzyParameters.EmptyString:
                return string.Empty;

            case FuzzyParameters.Null:
                return null;

            case FuzzyParameters.ThreeSpaces:
                return "   ";

            case FuzzyParameters.TwoChars:
                return "as";

            default:
                throw new ArgumentException(nameof(parameterValue));
        }
    }
}