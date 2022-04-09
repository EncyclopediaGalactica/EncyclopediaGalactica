namespace EncyclopediaGalactica.SourceFormats.QA.E2E.BackgroundSteps;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Sdk.Models;
using Sdk.Models.SourceFormatNode;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Utils.Guards;
using Xunit;

[Binding]
[ExcludeFromCodeCoverage]
public partial class BackgroundSteps
{
    private const string ENDPOINT_URL = "endpoint_url";
    private const string OPERATION_URL = "operation_url";
    private const string SOURCEFORMATNODE_NAME = "sourceformatnode_name";
    private const string SDK_OPERATION_BUILDER = "sdk_operation_builder";
    private const string SDK_OPERATION_RESULT = "sdk_operation_result";
    private readonly ScenarioContext _scenarioContext;

    [Given(@"there is the following endpoint")]
    public void GivenThereIsTheFollowingEndpoint(Table table)
    {
        Guards.IsNotNull(table);

        GivenThereIsTheFollowingEndpointEntity? ins = table.CreateInstance<GivenThereIsTheFollowingEndpointEntity>();
        Guards.IsNotNull(ins);
        Guards.StringIsNotNullOrEmptyOrWhitespace(ins.Url);

        _scenarioContext.Add(ENDPOINT_URL, ins.Url);
    }

    [Given(@"there is the operation endpoint")]
    public void GivenThereIsTheOperationEndpoint(Table table)
    {
        Guards.IsNotNull(table);
        GivenThereIsTheOperationEndpointEntity? entity = table.CreateInstance<GivenThereIsTheOperationEndpointEntity>();

        Guards.IsNotNull(entity);
        Guards.StringIsNotNullOrEmptyOrWhitespace(entity.Url);
        _scenarioContext.Add(OPERATION_URL, entity.Url);
    }

    [Given(@"the following SourceFormatNode data")]
    public void GivenTheFollowingSourceFormatNodeData(Table table)
    {
        Guards.IsNotNull(table);

        GivenTheFollowingSourceFormatNodeDataEntity entity = table
            .CreateInstance<GivenTheFollowingSourceFormatNodeDataEntity>();

        Guards.IsNotNull(entity);
        Guards.StringIsNotNullOrEmptyOrWhitespace(entity.Name);
        _scenarioContext.Add(SOURCEFORMATNODE_NAME, entity.Name);
    }

    [When(@"SourceFormatNode is sent to endpoint")]
    public async Task WhenSourceFormatNodeIsSentToEndpoint()
    {
        SourceFormatNodeAddRequestModel addRequestModel = ProvideSourceFormatNodeAddModel(_scenarioContext);
    }

    private SourceFormatNodeAddRequestModel ProvideSourceFormatNodeAddModel(ScenarioContext scenarioContext)
    {
        string name = GetValueFromSpecflowBucket<string>(SOURCEFORMATNODE_NAME);
        SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName(name)
            .Build();
        return requestModel;
    }

    private TType GetValueFromSpecflowBucket<TType>(string key)
    {
        Guards.StringIsNotNullOrEmptyOrWhitespace(key);
        TType result = (TType)_scenarioContext[key];
        return result;
    }

    [Given(@"the '(.*)' string parameter value is '(.*)'")]
    public void GivenTheStringParameterValueIs(string parameterName, string parameterValue)
    {
        Guards.StringIsNotNullOrEmptyOrWhitespace(parameterName);
        Guards.StringIsNotNullOrEmptyOrWhitespace(parameterValue);
        string? stringParameter = ConvertFuzzyValuesToParameterValue(parameterValue);
        SourceFormatNodeAddRequestModel.Builder builder = GetFromContext<SourceFormatNodeAddRequestModel.Builder>(
            SDK_OPERATION_BUILDER);
        builder.SetName(stringParameter!);
        OverWriteKeyInContext(SDK_OPERATION_BUILDER, builder);
    }

    [When(@"I prepare the data to be sent")]
    public void WhenIPrepareTheDataToBeSent()
    {
        SourceFormatNodeAddRequestModel.Builder builder = GetFromContext<SourceFormatNodeAddRequestModel.Builder>(
            SDK_OPERATION_BUILDER);
        try
        {
            SourceFormatNodeAddRequestModel model = builder.Build();
            Assert.False(true);
        }
        catch (Exception e)
        {
            AddToContext(SDK_OPERATION_RESULT, e);
        }
    }

    [Given(@"there is the Source Format SDK providing '(.*)' functionality")]
    public void GivenThereIsTheSourceFormatSdkProvidingFunctionality(string functionality)
    {
        switch (functionality)
        {
            case Operations.ADD_NEW_SOURCEFORMATNODE:
                SourceFormatNodeAddRequestModel.Builder builder = new SourceFormatNodeAddRequestModel.Builder();
                _scenarioContext.Add(SDK_OPERATION_BUILDER, builder);
                break;
        }
    }

    [Then(@"the SDK throws '(.*)'")]
    public void ThenTheSdkThrows(string exceptionType)
    {
        switch (exceptionType)
        {
            case ExceptionTypes.SDK_MODELS_EXCEPTION:
                object exception = _scenarioContext[SDK_OPERATION_RESULT];
                exception.GetType().ToString().Should().Be(typeof(SdkModelsException).ToString());
                break;
        }
    }

    internal struct Operations
    {
        public const string ADD_NEW_SOURCEFORMATNODE = "add_new_sourceformatnode";
    }

    internal struct ExceptionTypes
    {
        public const string SDK_MODELS_EXCEPTION = "SdkModelsException";
    }

    private class GivenTheFollowingSourceFormatNodeDataEntity
    {
        public string Name { get; set; }
    }

    private class GivenThereIsTheOperationEndpointEntity
    {
        public string Url { get; set; }
    }

    private class GivenThereIsTheFollowingEndpointEntity
    {
        public string Url { get; set; }
    }
}