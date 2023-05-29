namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeGetAllResponseModel;

using System.Collections.Generic;
using System.Net;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;
using FluentAssertions;
using Xunit;

public class SourceFormatNodeGetAllResponseModelShould
{
    [Fact]
    public void Throw_WhenHttpStatusCode_IsNotProvided()
    {
        // Arrange
        string message = "asd";
        HttpStatusCode code = HttpStatusCode.Accepted;

        // Act
        SourceFormatNodeGetAllResponseModel model = new SourceFormatNodeGetAllResponseModel.Builder()
            .SetMessage(message)
            .SetResult(new List<SourceFormatNodeDto>())
            .SetOperationSuccessful()
            .SetHttpStatusCode(code)
            .Build();

        // Assert
        model.Message.Should().NotBeNull();
        model.Message.Should().BeOfType<string>();
        model.Message.Should().Be(message);
        model.Result.Should().NotBeNull();
        model.Result.Should().BeOfType<List<SourceFormatNodeDto>>();
        model.IsOperationSuccessful.Should().Be(true);
    }
}