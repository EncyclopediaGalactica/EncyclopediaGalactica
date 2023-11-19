namespace EncyclopediaGalactica.Services.Document.Tests.Datasets.DocumentDto;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Contracts.Input;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class AddDocumentDto_Add_ValidDataForMandatoryFields : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new DocumentGraphqlInput
            {
                Name = "name",
                Description = "description"
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}