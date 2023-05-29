namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeDeleteResponseModel;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class SourceFormatNodeDeleteResponseModelValiationShould
{
    [Fact]
    public void Throw_WhenHttpStatusCode_IsNotProvided()
    {
        // Act
        Action action = () =>
        {
            SourceFormatNodeDeleteResponseModel m = new SourceFormatNodeDeleteResponseModel.Builder()
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
            SourceFormatNodeDeleteResponseModel m = new SourceFormatNodeDeleteResponseModel.Builder()
                .SetHttpStatusCode(HttpStatusCode.Accepted)
                .SetResult(new SourceFormatNodeDto())
                .SetOperationSuccessful()
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }
}