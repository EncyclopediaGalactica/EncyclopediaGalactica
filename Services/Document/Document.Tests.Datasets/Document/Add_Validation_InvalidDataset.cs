namespace EncyclopediaGalactica.Services.Document.Tests.Datasets.Document;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Entities;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class Add_Validation_InvalidDataset : IEnumerable<object[]>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new Document
            {
                Id = 1,
                Name = "asd",
                Description = "asd"
            }
        };
        yield return new object[]
        {
            new Document
            {
                Id = 0,
                Name = null,
                Description = "asd"
            }
        };
        yield return new object[]
        {
            new Document
            {
                Id = 0,
                Name = string.Empty,
                Description = "asd"
            }
        };
        yield return new object[]
        {
            new Document
            {
                Id = 1,
                Name = "   ",
                Description = "asd"
            }
        };
        yield return new object[]
        {
            new Document
            {
                Id = 1,
                Name = "asd",
                Description = null
            }
        };
        yield return new object[]
        {
            new Document
            {
                Id = 1,
                Name = "asd",
                Description = string.Empty
            }
        };
    }
}