namespace DocumentDomain.Spec.Operations.Scenario.DocumentType.Data;

using System.Collections;
using EncyclopediaGalactica.Common.Contracts;

public class DeleteDocumentTypeScenarioInputValidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new[]
        {
            new DocumentTypeInput { Id = 1 }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}