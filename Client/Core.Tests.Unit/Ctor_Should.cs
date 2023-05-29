namespace EncyclopediaGalactica.Client.Core.Tests.Unit;

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class CtorShould
{
    [Fact]
    public void Throw_WhenArgumentIsNull()
    {
        // Arrange and Assert
        Action action = () => { new SdkCore(null!); };

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }
}