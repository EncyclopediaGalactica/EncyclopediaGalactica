namespace EncyclopediaGalactica.Services.Document.Tests.Datasets.DocumentDto;

using System.Collections;
using Dtos;

public class UpdateDocumentDto_Update_Dataset : IEnumerable<object[]>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 100,
                Name = "only the name changes",
                Description = "_default_"
            }
        };
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 100,
                Name = "_default_",
                Description = "only the description changes"
            }
        };
        yield return new object[]
        {
            new DocumentDto
            {
                Id = 100,
                Name = "both changes",
                Description = "both changes"
            }
        };
    }
}