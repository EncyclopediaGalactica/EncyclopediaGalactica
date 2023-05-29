namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeAddChildToParentResponseModel;

using System;
using System.Net;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;
using FluentAssertions;
using Xunit;

public class SourceFormatNodeAddChildToParentResponseModelValidationShould
{
    [Fact]
    public void Throw_WhenHttpStatusCode_IsNotProvided()
    {
        // Act
        Action action = () =>
        {
            SourceFormatNodeAddChildToParentResponseModel m =
                new SourceFormatNodeAddChildToParentResponseModel.Builder()
                    .SetMessage("asd")
                    .SetResult(new SourceFormatNodeDto())
                    .SetOperationSuccessful()
                    .Build();
        };

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Throw_WhenMessage_IsNotProvided()
    {
        // Act
        Action action = () =>
        {
            SourceFormatNodeAddChildToParentResponseModel m =
                new SourceFormatNodeAddChildToParentResponseModel.Builder()
                    .SetHttpStatusCode(HttpStatusCode.Accepted)
                    .SetResult(new SourceFormatNodeDto())
                    .SetOperationSuccessful()
                    .Build();
        };

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }
}