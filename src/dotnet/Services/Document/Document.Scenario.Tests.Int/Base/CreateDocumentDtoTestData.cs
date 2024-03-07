namespace EncyclopediaGalactica.Services.Document.Scenario.Tests.Int.Base;

using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Input;
using Contracts.Output;

public partial class BaseTest
{
    protected async Task<List<long>> CreateDocumentDtoTestData(int amount)
    {
        List<long> result = new List<long>();
        for (int i = 0; i < amount; i++)
        {
            DocumentResult res = await AddDocumentScenario.AddAsync(
                    new DocumentInput
                    {
                        Name = $"_default_{i}",
                        Description = $"_default_{i}"
                    })
                .ConfigureAwait(false);
            result.Add(res.Id);
        }

        return result;
    }
}