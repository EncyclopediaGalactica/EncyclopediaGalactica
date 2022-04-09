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
    private readonly ScenarioContext _scenarioContext;

    [Given(@"there is the following endpoint")]
    public void GivenThereIsTheFollowingEndpoint(TechTalk.SpecFlow.Table table)
    {
        Guards.IsNotNull(table);

        GivenThereIsTheFollowingEndpointEntity? ins = table.CreateInstance<GivenThereIsTheFollowingEndpointEntity>();
        Guards.IsNotNull(ins);
        Guards.StringIsNotNullOrEmptyOrWhitespace(ins.Url);

        _scenarioContext.Add(Keys.EndpointUrl, ins.Url);
    }

    [Given(@"there is the operation endpoint")]
    public void GivenThereIsTheOperationEndpoint(TechTalk.SpecFlow.Table table)
    {
        Guards.IsNotNull(table);
        GivenThereIsTheOperationEndpointEntity? entity = table.CreateInstance<GivenThereIsTheOperationEndpointEntity>();

        Guards.IsNotNull(entity);
        Guards.StringIsNotNullOrEmptyOrWhitespace(entity.Url);
        _scenarioContext.Add(Keys.OperationUrl, entity.Url);
    }

    [Given(@"the following SourceFormatNode data")]
    public void GivenTheFollowingSourceFormatNodeData(TechTalk.SpecFlow.Table table)
    {
        Guards.IsNotNull(table);

        GivenTheFollowingSourceFormatNodeDataEntity entity = table
            .CreateInstance<GivenTheFollowingSourceFormatNodeDataEntity>();

        Guards.IsNotNull(entity);
        Guards.StringIsNotNullOrEmptyOrWhitespace(entity.Name);
        _scenarioContext.Add(Keys.SourceFormatNodeName, entity.Name);
    }

    [When(@"SourceFormatNode is sent to endpoint")]
    public async Task WhenSourceFormatNodeIsSentToEndpoint()
    {
        SourceFormatNodeAddRequestModel addRequestModel = ProvideSourceFormatNodeAddModel(_scenarioContext);
    }

    private SourceFormatNodeAddRequestModel ProvideSourceFormatNodeAddModel(ScenarioContext scenarioContext)
    {
        string name = GetValueFromSpecflowBucket<string>(Keys.SourceFormatNodeName);
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
            Keys.SdkRequestModelBuilder);
        builder.SetName(stringParameter!);
        OverWriteKeyInContext(Keys.SdkRequestModelBuilder, builder);
    }

    [When(@"I prepare the data to be sent")]
    public void WhenIPrepareTheDataToBeSent()
    {
        SourceFormatNodeAddRequestModel.Builder builder = GetFromContext<SourceFormatNodeAddRequestModel.Builder>(
            Keys.SdkRequestModelBuilder);
        try
        {
            SourceFormatNodeAddRequestModel model = builder.Build();
            Assert.False(true);
        }
        catch (Exception e)
        {
            AddToContext(Keys.SdkOperationResult, e);
        }
    }

    [Given(@"there is the '(.*)' SDK providing '(.*)' functionality")]
    public void GivenThereIsTheSourceFormatSdkProvidingFunctionality(string sdkType, string operationName)
    {
        Guards.StringIsNotNullOrEmptyOrWhitespace(sdkType);
        Guards.StringIsNotNullOrEmptyOrWhitespace(operationName);

        AddToContext(Keys.SdkType, sdkType);

        switch (operationName)
        {
            case Operations.AddNewSourceFormatNode:
                SourceFormatNodeAddRequestModel.Builder builder = new SourceFormatNodeAddRequestModel.Builder();
                _scenarioContext.Add(Keys.SdkRequestModelBuilder, builder);
                _scenarioContext.Add(Keys.SdkOperationName, operationName);
                break;
        }
    }

    [Then(@"the SDK throws '(.*)'")]
    public void ThenTheSdkThrows(string exceptionType)
    {
        switch (exceptionType)
        {
            case ExceptionTypes.SdkModelsException:
                object exception = _scenarioContext[Keys.SdkOperationResult];
                exception.GetType().ToString().Should().Be(typeof(SdkModelsException).ToString());
                break;
        }
    }

    [When(@"I send the data using '(.*)' SDK")]
    public async Task WhenISendTheDataUsingSdk(string sdkType)
    {
        Guards.StringIsNotNullOrEmptyOrWhitespace(sdkType);
        string operationName = GetFromContext<string>(Keys.SdkOperationName);

        switch (sdkType)
        {
            case SdkType.SourceFormatSdk:
                switch (operationName)
                {
                    case Operations.AddNewSourceFormatNode:
                        await MakeSourceFormatNodeSdkAddCall().ConfigureAwait(false);
                        break;

                    default:
                        throw new Exception();
                }

                break;

            default:
                throw new Exception();
        }
    }

    private async Task MakeSourceFormatNodeSdkAddCall()
    {
        try
        {
            SourceFormatNodeAddRequestModel model =
                GetFromContext<SourceFormatNodeAddRequestModel>(Keys.SdkRequestModel);
            SourceFormatNodeAddResponseModel result = await SourceFormatsSdk.SourceFormatNode
                .AddAsync(model)
                .ConfigureAwait(false);
            AddToContext(Keys.SdkOperationResult, result);
        }
        catch (Exception e)
        {
            AddToContext(Keys.SdkOperationResult, e);
        }
    }

    [Given(@"I prepare and store the data to be sent")]
    public void GivenIPrepareAndStoreTheDataToBeSent()
    {
        string operation = GetFromContext<string>(Keys.SdkOperationName);
        switch (operation)
        {
            case Operations.AddNewSourceFormatNode:
                SourceFormatNodeAddRequestModel.Builder builder = GetFromContext<SourceFormatNodeAddRequestModel
                    .Builder>(Keys.SdkRequestModelBuilder);
                SourceFormatNodeAddRequestModel model = builder.Build();
                AddToContext(Keys.SdkRequestModel, model);
                break;
        }
    }

    [Then(@"I get an response")]
    public void ThenIGetAnResponse()
    {
        string sdkType = GetFromContext<string>(Keys.SdkType);
        string operationName = GetFromContext<string>(Keys.SdkOperationName);

        switch (sdkType)
        {
            case SdkType.SourceFormatSdk:
                switch (operationName)
                {
                    case Operations.AddNewSourceFormatNode:
                        CheckResponseModelType<SourceFormatNodeAddResponseModel>();
                        break;

                    default:
                        throw new Exception();
                }

                break;

            default:
                throw new Exception();
        }
    }

    private void CheckResponseModelType<T>()
    {
        object responseModel = _scenarioContext[Keys.SdkOperationResult];
        responseModel.GetType().ToString().Should().BeOfType<T>();
    }
}