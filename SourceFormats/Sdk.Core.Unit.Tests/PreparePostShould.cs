namespace Sdk.Core.Unit.Tests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class PreparePostShould
{
    [Theory]
    [InlineData(null, "asd")]
    [InlineData("string", null)]
    [InlineData("string", "")]
    [InlineData("string", " ")]
    public void Throw_WhenInputIsNullOrEmptyOrWhitespace(object dto, string url)
    {
        // Arrange
        SdkCore sdkCore = new SdkCore(new HttpClient());

        // Act
        Action action = () => { sdkCore.PreparePost(dto, url); };

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }
}