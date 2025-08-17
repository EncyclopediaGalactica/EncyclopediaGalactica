namespace EncyclopediaGalactica.Scenarios.Storage.Vertex;

using Common;
using EncyclopediaGalactica.Storage;
using EncyclopediaGalactica.Storage.Repository;
using LanguageExt;

public class ListVerticesScenario(VertexRepository vertexRepository, StorageContext ctx)
{
    public Either<EgError, Unit> Execute()
    {
        return from _ in vertexRepository.GetAll(ctx)
                .Do(list =>
                {
                    list.ToList().ForEach(item =>
                    {
                        Console.WriteLine($"item: {item.Id}, {item.Data}");
                    });
                    
                })
            select Unit.Default;
    }
    
}