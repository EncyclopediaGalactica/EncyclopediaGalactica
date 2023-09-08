namespace EncyclopediaGalactica.Services.Document.Tests.Datasets.Document;

using System.Collections;
using Dtos;

public class AddDocumentValidDataForMandatoryFields : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new DocumentDto
            {
                Name = "name",
                Description = "description"
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}