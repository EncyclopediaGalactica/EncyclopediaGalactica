namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.Base;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

public partial class BaseTest
{
    protected async Task<List<long>> CreateDocumentEntities(int amount)
    {
        List<long> result = new List<long>();
        for (int i = 0; i < amount; i++)
        {
            Document res = await Sut.Documents.AddAsync(new Document
            {
                Name = $"_default_{i}",
                Description = $"_default_{i}",
                Uri = new Uri($"https://default{i}.com")
            }).ConfigureAwait(false);
            result.Add(res.Id);
        }

        return result;
    }
}