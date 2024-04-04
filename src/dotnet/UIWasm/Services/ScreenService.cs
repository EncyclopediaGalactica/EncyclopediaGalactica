namespace UIWasm.Services;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public class ScreenService : IScreenService
{
    private readonly List<ScreenResult> _storage = new List<ScreenResult>
    {
        new ScreenResult { Id = 100, Name = "Documents", ModuleId = 1 },
        new ScreenResult { Id = 101, Name = "Relations", ModuleId = 1 },
        new ScreenResult { Id = 102, Name = "Incomes", ModuleId = 2 },
        new ScreenResult { Id = 103, Name = "Expenses", ModuleId = 2 },
        new ScreenResult { Id = 104, Name = "Stellar items", ModuleId = 3 },
        new ScreenResult { Id = 105, Name = "Route planner", ModuleId = 3 },
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