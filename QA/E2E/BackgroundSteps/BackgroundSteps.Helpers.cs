namespace EncyclopediaGalactica.SourceFormats.QA.E2E.BackgroundSteps;

using System;
using System.Reflection;
using FluentAssertions;
using Sdk.Models.SourceFormatNode;
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
                return parameterValue;
        }
    }

    private void PossibleStructValues<T>()
    {
        _testOutputHelper.WriteLine($"Values of {typeof(T)} struct.");
        PropertyInfo[] props = typeof(T).GetProperties();
        foreach (PropertyInfo propertyInfo in props)
        {
            _testOutputHelper.WriteLine(propertyInfo.Name);
        }
    }

    private void ExecuteIsCheck(
        SourceFormatNodeAddResponseModel responseModel,
        string propertyName,
        string expectedStatus)
    {
        switch (propertyName.ToLower())
        {
            case ResponseModelProperties.Result:

                switch (expectedStatus)
                {
                    case IsCheckOperations.IsNotNull:
                        responseModel.Result.Should().NotBeNull();
                        break;

                    default:
                        PossibleStructValues<IsCheckOperations>();
                        throw new Exception();
                }

                break;

            default:
                PossibleStructValues<ResponseModelProperties>();
                throw new Exception();
        }
    }

    private void ExecuteEqualsToCheck(SourceFormatNodeAddResponseModel responseModel, string propertyName,
        string expectedStatus)
    {
        switch (propertyName.ToLower())
        {
            case ResponseModelProperties.HttpStatusCode:

                switch (expectedStatus)
                {
                    case EqualsToCheckOperations.EqualsTo:
                        responseModel.HttpStatusCode.ToString().Should().Be(expectedStatus);
                        break;

                    default:
                        PossibleStructValues<EqualsToCheckOperations>();
                        throw new Exception();
                }

                break;

            case ResponseModelProperties.IsOperationSuccessful:
                switch (expectedStatus)
                {
                    case EqualsToCheckOperations.EqualsTo:
                        responseModel.IsOperationSuccessful.ToString().Should().Be(expectedStatus);
                        break;

                    default:
                        PossibleStructValues<EqualsToCheckOperations>();
                        throw new Exception();
                }

                break;

            default:
                PossibleStructValues<ResponseModelProperties>();
                throw new Exception();
        }
    }
}