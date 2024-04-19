using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

public class ApplicationService : IApplicationService
{
    private ICollection<ApplicationResult> _storage = new List<ApplicationResult>
    {
        new ApplicationResult
        {
            Id = 1,
            Name = "General",
            Description = "General usage"
        },
        new ApplicationResult
        {
            Id = 2,
            Name = "Finance",
            Description = "Finance information"
        },
        new ApplicationResult
        {
            Id = 3,
            Name = "StarMap",
            Description = "StarMap"
        },
    };

    public async Task<ICollection<ApplicationResult>> GetAllAsync()
    {
        return _storage;
    }
}