#region

#endregion

namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public class ScreenService : IScreenService
{
    private readonly List<ScreenResult> _storage = new List<ScreenResult>
    {
        new ScreenResult { Id = 100, Name = "Document Structures", UnifiedName = "document_structure", ModuleId = 1 },
        new ScreenResult { Id = 120, Name = "Document Type", UnifiedName = "document_type", ModuleId = 1 },
        new ScreenResult { Id = 130, Name = "Documents Catalog", UnifiedName = "document_catalog", ModuleId = 1 },
        new ScreenResult { Id = 140, Name = "File format", UnifiedName = "file_format", ModuleId = 1 },
        new ScreenResult { Id = 150, Name = "Relations", UnifiedName = "relations", ModuleId = 1 },
        new ScreenResult { Id = 101, Name = "Relation Types", UnifiedName = "relation_types", ModuleId = 1 },
        new ScreenResult { Id = 110, Name = "Applications", UnifiedName = "applications", ModuleId = 1 },
        new ScreenResult
        {
            Id = 150,
            Name = "Tree Editor",
            UnifiedName = "document_structure_tree_editor",
            ModuleId = 1
        },

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