namespace DocumentDomain.Spec.Operations.Scenario.Relation;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;
using FluentAssertions;
using LanguageExt;

public class AddRelationScenarioShould : ScenarioBaseTest
{
    [Fact]
    public async Task AddNewRelation_AndReturnWithIt()
    {
        RelationInput input = new RelationInput { Id = 0, LeftEndId = 1, RightEndId = 1 };
        AddRelationScenarioContext context = new(Guid.NewGuid(), input);

        Either<ErrorResult, RelationResult> result = await AddRelationScenario.ExecuteAsync(context);

        result.IsRight.Should().BeTrue();
        result.IsLeft.Should().BeFalse();
        result.IfRight(r =>
        {
            r.Id.Should().BeGreaterOrEqualTo(1);
            r.LeftDocumentId.Should().Be(input.LeftEndId);
            r.RightDocumentId.Should().Be(input.RightEndId);
        });
    }

    [Fact]
    public async Task ReturnErrorResult_WhenOperationIsUnsuccessful()
    {
        RelationInput input = new RelationInput { Id = 0, LeftEndId = 0, RightEndId = 1 };
        AddRelationScenarioContext context = new(Guid.NewGuid(), input);

        Either<ErrorResult, RelationResult> result = await AddRelationScenario.ExecuteAsync(context);

        result.IsRight.Should().BeFalse();
        result.IsLeft.Should().BeTrue();
        result.IfLeft(error =>
        {
            error.CorrelationId.Should().NotBeEmpty();
            error.ErrorMessage.Should().NotBeEmpty();
        });
    }
}