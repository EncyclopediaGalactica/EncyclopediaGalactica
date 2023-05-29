namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeGetAllResponseModel;

using System;
using System.Collections.Generic;
using System.Net;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;
using FluentAssertions;
using Xunit;

public class SourceFormatNodeGetAllResponseModelValidationShould
{
    [Fact]
    public void Throw_WhenHttpStatusCode_IsNotProvided()
    {
        // Act
        Action action = () =>
        {
            SourceFormatNodeGetAllResponseModel m = new SourceFormatNodeGetAllResponseModel.Builder()
                .SetMessage("asd")
                .SetResult(new List<SourceFormatNodeDto>())
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
            SourceFormatNodeGetAllResponseModel m = new SourceFormatNodeGetAllResponseModel.Builder()
                .SetHttpStatusCode(HttpStatusCode.Accepted)
                .SetResult(new List<SourceFormatNodeDto>())
                .SetOperationSuccessful()
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }
}