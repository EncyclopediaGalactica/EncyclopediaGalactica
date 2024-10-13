namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public class RelationTypeService : IRelationTypeService
{
    private ICollection<RelationTypeResult> _storage = new List<RelationTypeResult>
    {
        new RelationTypeResult
        {
            Id = 1,
            Name = "BelongsTo",
            Description = "The item at the right side belongs to the item at the left side."
        },
        new RelationTypeResult
        {
            Id = 2,
            Name = "Extends",
            Description = "The item at the right side extends the item at the left side."
        },
        new RelationTypeResult
        {
            Id = 3,
            Name = "RelatesTo",
            Description = "Describes a connection between two related document."
        }
    };

    public async Task<ICollection<RelationTypeResult>> GetAllAsync()
    {
        return _storage;
    }
}