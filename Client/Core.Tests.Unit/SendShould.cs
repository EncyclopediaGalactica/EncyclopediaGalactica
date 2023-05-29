namespace EncyclopediaGalactica.Client.Core.Tests.Unit;

using System.Diagnostics.CodeAnalysis;
using Xunit;

[ExcludeFromCodeCoverage]
public class SendShould
{
    [Fact]
    public void Throw_WhenInputIsNull()
    {
        // // Arrange
        // SdkCore sdkCore = new SdkCore(new HttpClient());
        //
        // // Act
        // Func<Task> task = async () => { await sdkCore.SendAsync<int, int>(null!); };
        //
        // // Assert
        // task.Should().ThrowExactlyAsync<SourceFormatsSdkCoreException>();
    }
}