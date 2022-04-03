using Program = Host.Program;

namespace EncyclopediaGalactica.SourceFormats.QA.E2E.BackgroundSteps;

using TechTalk.SpecFlow;
using Utils.Guards;

public partial class BackgroundSteps : TestBase
{
    public BackgroundSteps(
        SourceFormatWebApplicationFactory<Program> webApplicationFactory,
        ScenarioContext scenarioContext) : base(webApplicationFactory)
    {
        Guards.IsNotNull(scenarioContext);
        _scenarioContext = scenarioContext;
    }
}