namespace IAM.Entities.Tests.Unit;

using FluentAssertions;
using Microsoft.VisualBasic;
using Xunit;

public class ModuleShould
{
    [Fact]
    public void DefaultValues()
    {
        // Act
        Module module = new Module();

        // Assert
        module.Id.Should().Be(0);
        module.Name.Should().BeEmpty();
        module.Description.Should().BeEmpty();
        module.LastModified.Should().Be(new DateTime());
        module.LastModifiedBy.Should().Be(0);
        module.Created.Should().Be(new DateTime());
        module.CreatedBy.Should().Be(0);
    }

    [Fact]
    public void NoValueChangingLogicForProperties()
    {
        // Arrange
        long moduleId = 100;
        string moduleName = "Name";
        string moduleDescription = "Description";
        DateTime lastModified = DateTime.Now;
        long lastModifiedBy = 101;
        DateTime created = DateTime.Today;
        long createdBy = 102;

        // Act
        Module module = new Module
        {
            Id = moduleId,
            Name = moduleName,
            Description = moduleDescription,
            LastModified = lastModified,
            LastModifiedBy = lastModifiedBy,
            Created = created,
            CreatedBy = createdBy
        };

        // Assert
        module.Id.Should().Be(moduleId);
        module.Name.Should().Be(moduleName);
        module.Description.Should().Be(moduleDescription);
        module.LastModified.Should().Be(lastModified);
        module.LastModifiedBy.Should().Be(lastModifiedBy);
        module.Created.Should().Be(created);
        module.CreatedBy.Should().Be(createdBy);

    }
}