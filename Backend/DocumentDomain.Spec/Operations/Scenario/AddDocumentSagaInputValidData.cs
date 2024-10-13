namespace DocumentDomain.Spec.Operations.Scenario;

using System.Collections;
using EncyclopediaGalactica.Common.Contracts;

public class AddDocumentSagaInputValidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new DocumentInput { Id = 0, Name = "asd", Description = "asd" }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}