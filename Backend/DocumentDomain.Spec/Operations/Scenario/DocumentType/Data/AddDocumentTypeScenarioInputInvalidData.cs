namespace DocumentDomain.Spec.Operations.Scenario.DocumentType.Data;

using System.Collections;
using EncyclopediaGalactica.Common.Contracts;

public class AddDocumentTypeScenarioInputInvalidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new DocumentTypeInput { Id = 1, Name = "asd", Description = "asd" }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = null, Description = "asd" }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = string.Empty, Description = "asd" }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = "   ", Description = "asd" }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = "as   ", Description = "asd" }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = "asd", Description = null }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = "asd", Description = string.Empty }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = "asd", Description = "     " }
        };
        yield return new object[]
        {
            new DocumentTypeInput { Id = 0, Name = "asd", Description = "as     " }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}