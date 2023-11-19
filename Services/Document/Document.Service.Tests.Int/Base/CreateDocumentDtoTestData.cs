namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Base;

using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Input;

public partial class BaseTest
{
    protected async Task<List<long>> CreateDocumentDtoTestData(int amount)
    {
        List<long> result = new List<long>();
        for (int i = 0; i < amount; i++)
        {
            DocumentGraphqlInput res = await Sut.DocumentService.AddAsync(
                    new DocumentGraphqlInput
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