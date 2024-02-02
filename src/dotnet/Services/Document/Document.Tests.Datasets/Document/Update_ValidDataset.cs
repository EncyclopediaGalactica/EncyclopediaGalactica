namespace EncyclopediaGalactica.Services.Document.Tests.Datasets.Document;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Entities;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class Update_ValidDataset : IEnumerable<object[]>
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
                Id = 1,
                Name = "whatever",
                Description = "whatever"
            }
        };
    }
}