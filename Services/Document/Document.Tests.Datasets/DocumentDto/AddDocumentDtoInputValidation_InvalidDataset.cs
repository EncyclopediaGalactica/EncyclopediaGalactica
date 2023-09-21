namespace EncyclopediaGalactica.Services.Document.Tests.Datasets.DocumentDto;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Dtos;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class AddDocumentDtoInputValidation_InvalidDataset : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 1,
                Name = "name",
                Description = "description"
            }
        };
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 0,
                Name = string.Empty,
                Description = "description"
            }
        };
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 0,
                Name = null,
                Description = "description"
            }
        };
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 0,
                Name = "na",
                Description = "description"
            }
        };
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 0,
                Name = "na ",
                Description = "description"
            }
        };
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 0,
                Name = "   ",
                Description = "description"
            }
        };
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 0,
                Name = "name",
                Description = null
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}