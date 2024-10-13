namespace DocumentDomain.Spec.Operations.Scenario.DocumentType.Data;

using System.Collections;
using EncyclopediaGalactica.Common.Contracts;

public class GetDocumentTypesScenarioData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<DocumentTypeInput>
            {
                new DocumentTypeInput { Name = "name1", Description = "desc1" },
            }
        };
        yield return new object[]
        {
            new List<DocumentTypeInput>
            {
                new DocumentTypeInput { Name = "name1", Description = "desc1" },
                new DocumentTypeInput { Name = "name2", Description = "desc2" },
            }
        };
        yield return new object[]
        {
            new List<DocumentTypeInput>
            {
                new DocumentTypeInput { Name = "name1", Description = "desc1" },
                new DocumentTypeInput { Name = "name2", Description = "desc2" },
                new DocumentTypeInput { Name = "name3", Description = "desc3" },
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}