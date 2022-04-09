namespace EncyclopediaGalactica.SourceFormats.Sdk.Core.Unit.Tests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using EncyclopediaGalactica.Sdk.Core;
using EncyclopediaGalactica.Sdk.Core.Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class SendShould
{
    [Fact]
    public void Throw_WhenInputIsNull()
    {
        // Arrange
        SdkCore sdkCore = new SdkCore(new HttpClient());

        // Act
        Func<Task> task = async () => { await sdkCore.SendAsync<int, int>(null!); };

        // Assert
        task.Should().ThrowExactlyAsync<SourceFormatsSdkCoreException>();
    }
}