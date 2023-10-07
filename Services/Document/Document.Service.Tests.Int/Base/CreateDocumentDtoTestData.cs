namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Base;

using System.Collections.Generic;
using System.Threading.Tasks;
using Dtos;

public partial class BaseTest
{
    protected async Task<List<long>> CreateDocumentDtoTestData(int amount)
    {
        List<long> result = new List<long>();
        for (int i = 0; i < amount; i++)
        {
            DocumentDto res = await Sut.DocumentService.AddAsync(
                    new DocumentDto
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