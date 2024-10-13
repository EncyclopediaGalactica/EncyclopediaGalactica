namespace DocumentDomain.Spec.Operations.Scenario.DocumentType.Data;

using System.Collections;
using EncyclopediaGalactica.Common.Contracts;

public class AddDocumentTypeScenarioInputValidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = "asd", Description = "asd" }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = "longer name", Description = "asd" }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = "longer name", Description = "longer descriptio" }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}