namespace DocumentDomain.Spec.Operations.Scenario.DocumentType.Data;

using System.Collections;
using EncyclopediaGalactica.Common.Contracts;

public class UpdateDocumentTypeScenarioInputValidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new DocumentTypeInput { Id = 1, Name = "asd", Description = "asd" }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 1, Name = "longer name", Description = "asd" }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 1, Name = "longer name", Description = "longer description" }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}