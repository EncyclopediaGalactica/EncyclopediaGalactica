namespace Sdk.Core.Unit.Tests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Exceptions;
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
        Func<Task> task = async () => { await sdkCore.SendAsync<int>(null!); };

        // Assert
        task.Should().ThrowExactlyAsync<SourceFormatsSdkCoreException>();
    }
}