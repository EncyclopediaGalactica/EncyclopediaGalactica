namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using FluentAssertions;
using FluentValidation;
using Services.Document.Tests.Datasets.DocumentDto;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class Add_Validation_Should : BaseTest
{
    [Fact]
    public void Throw_WhenInput_IsNull_GraphqlInput()
    {
        // Arrange && Act
        Func<Task> f = async () =>
        {
            await Sut.DocumentService.AddAsync((Graphql.Types.Input.DocumentGraphqlInputType)null!);
        };
    }

    [Fact]
    public void Throw_WhenInput_IsNull_DocumentInput()
    {
        // Arrange && Act
        Func<Task> f = async () => { await Sut.DocumentService.AddAsync((Contracts.Input.DocumentGraphqlInput)null!); };
    }

    [Theory]
    [ClassData(typeof(AddDocumentDtoInputValidation_InvalidDataset))]
    public void Throw_ValidationException_WhenInput_IsInvalid(
        Contracts.Input.DocumentGraphqlInput graphqlInputGraphqlInput)
    {
        // Arrange && Act
        Func<Task> f = async () => { await Sut.DocumentService.AddAsync(graphqlInputGraphqlInput); };

        // Assert
        f.Should().ThrowExactlyAsync<ValidationException>();
    }
}