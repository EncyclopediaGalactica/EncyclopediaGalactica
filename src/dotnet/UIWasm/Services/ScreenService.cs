namespace UIWasm.Services;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public class ScreenService : IScreenService
{
    private readonly List<ScreenResult> _storage = new List<ScreenResult>
    {
        new ScreenResult { Id = 100, Name = "Documents", UnifiedName = "documents", ModuleId = 1 },
        new ScreenResult { Id = 101, Name = "Relations", UnifiedName = "relations", ModuleId = 1 },
        new ScreenResult { Id = 102, Name = "Incomes", UnifiedName = "incomes", ModuleId = 2 },
        new ScreenResult { Id = 103, Name = "Expenses", UnifiedName = "expenses", ModuleId = 2 },
        new ScreenResult { Id = 104, Name = "Stellar items", UnifiedName = "stellar_items", ModuleId = 3 },
        new ScreenResult { Id = 105, Name = "Route planner", UnifiedName = "route_planner", ModuleId = 3 },
        new ScreenResult { Id = 106, Name = "Users", UnifiedName = "users", ModuleId = 4 },
        new ScreenResult { Id = 107, Name = "Roles", UnifiedName = "roles", ModuleId = 4 },
    };

    public async Task<IEnumerable<ScreenResult>> GetAll()
    {
        return _storage;
    }

    public IEnumerable<ScreenResult> GetScreensOfModule(long moduleId)
    {
        return _storage.Where(w => w.ModuleId == moduleId).ToList();
    }
}