using Program = Host.Program;

namespace EncyclopediaGalactica.SourceFormats.QA.E2E.BackgroundSteps;

using TechTalk.SpecFlow;
using Utils.Guards;
using Xunit.Abstractions;

public partial class BackgroundSteps : TestBase
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BackgroundSteps(
        SourceFormatWebApplicationFactory<Program> webApplicationFactory,
        ScenarioContext scenarioContext,
        ITestOutputHelper outputHelper) : base(webApplicationFactory)
    {
        Guards.IsNotNull(scenarioContext);
        _scenarioContext = scenarioContext;

        Guards.IsNotNull(outputHelper);
        _testOutputHelper = outputHelper;
    }
}