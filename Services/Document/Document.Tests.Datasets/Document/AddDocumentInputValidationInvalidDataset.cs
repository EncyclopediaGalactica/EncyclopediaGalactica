namespace EncyclopediaGalactica.Services.Document.Tests.Datasets.Document;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Dtos;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class AddDocumentInputValidationInvalidDataset : IEnumerable<object[]>
{
    public IEnumerable<object[]> GetAddValidationScenarioDataset = new List<object[]>
    {
        new object[] { 1, "name", "desc", new Uri("https://asd.com") },
        new object[] { 0, string.Empty, "desc", new Uri("https://asd.com") },
        new object[] { 0, null, "desc", new Uri("https://asd.com") },
        new object[] { 0, "na", "desc", new Uri("https://asd.com") },
        new object[] { 0, "   ", "desc", new Uri("https://asd.com") },
        new object[] { 0, "name", string.Empty, new Uri("https://asd.com") },
    };

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