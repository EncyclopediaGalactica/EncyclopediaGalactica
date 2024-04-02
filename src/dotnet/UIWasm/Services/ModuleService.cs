namespace UIWasm.Services;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public class ModuleService : IModuleService
{
    private readonly IEnumerable<ModuleResult> _storage = new List<ModuleResult>
    {
        new ModuleResult { Id = 0, Name = "-- Select One --", Description = "-- Select One --" },
        new ModuleResult
        {
            Id = 4, Name = "Administration",
            UnifiedName = "administration",
            Description = "Administration"
        },
        new ModuleResult
        {
            Id = 1, Name = "Documents",
            UnifiedName = "documents",
            Description = "Documents"
        },
        new ModuleResult
        {
            Id = 2, Name = "Finance",
            UnifiedName = "finance",
            Description = "Finance"
        },
        new ModuleResult
        {
            Id = 3, Name = "StarMap",
            UnifiedName = "starmap",
            Description = "StarMap"
        },
    };

    public IEnumerable<ModuleResult> GetAll()
    {
        return _storage;
    }
}