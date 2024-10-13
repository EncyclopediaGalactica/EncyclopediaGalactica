namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public class RoleService : IRoleService
{
    private ICollection<RoleResult> _storage = new List<RoleResult>
    {
        new RoleResult { Id = 1, Name = "Administrator", Description = "God" },
        new RoleResult { Id = 2, Name = "Administrator 2", Description = "God 2" },
        new RoleResult { Id = 3, Name = "Administrator 3", Description = "God 3" },
        new RoleResult { Id = 4, Name = "Administrator 4", Description = "God 4" },
        new RoleResult { Id = 5, Name = "Administrator 5", Description = "God 5" },
    };

    public async Task<ICollection<RoleResult>> GetAll()
    {
        return _storage;
    }
}