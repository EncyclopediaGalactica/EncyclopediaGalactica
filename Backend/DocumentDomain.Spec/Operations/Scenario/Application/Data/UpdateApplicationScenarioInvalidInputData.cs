namespace DocumentDomain.Spec.Operations.Scenario.Application.Data;

using System.Collections;
using EncyclopediaGalactica.Common.Contracts;

public class UpdateApplicationScenarioInvalidInputData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 0,
                Name = "name",
                Description = "description"
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = string.Empty,
                Description = "description"
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = null,
                Description = "description"
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = " ",
                Description = "description"
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = "na",
                Description = "description"
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = " as ",
                Description = "description"
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = "name",
                Description = string.Empty
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = "name",
                Description = " "
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = "name",
                Description = "de"
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = " as ",
                Description = "de "
            }
        };
        yield return new object[]
        {
            new ApplicationInput
            {
                Id = 1,
                Name = " as ",
                Description = null
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}