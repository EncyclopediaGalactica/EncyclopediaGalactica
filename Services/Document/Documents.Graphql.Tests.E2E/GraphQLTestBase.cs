namespace Documents.Graphql.Tests.E2E;

using System.Text;
using FluentAssertions;
using HotChocolate.Execution;
using Xunit.Abstractions;

public class GraphQLTestBase
{
    protected readonly ITestOutputHelper _testOutputHelper;

    public GraphQLTestBase(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    protected void CheckResultForErrors(IQueryResult result)
    {
        if (result.Errors != null && result.Errors.Count > 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n === Errors === \n");

            for (int x = 0; x < result.Errors.Count; x++)
            {
                builder.Append($"= Error {x} = \n");
                builder.Append($"Message: {result.Errors[x].Message} \n");
            }

            builder.Append("\n === Errors End === \n");

            result.Errors.Count.Should().Be(0, builder.ToString());
        }
    }
}