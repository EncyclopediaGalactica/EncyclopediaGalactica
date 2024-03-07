namespace EncyclopediaGalactica.Services.Document.Repository.Structure;

using Ctx;
using Entities;
using FluentValidation;
using Interfaces;
using Microsoft.EntityFrameworkCore;

public partial class StructureNodeRepository : IStructureNodeRepository
{
    private readonly DbContextOptions<DocumentDbContext> _dbContextOptions;
    private readonly IValidator<StructureNode> _structureNodeValidator;

    public StructureNodeRepository(
        DbContextOptions<DocumentDbContext> dbContextOptions,
        IValidator<StructureNode> structureNodeValidator)
    {
        ArgumentNullException.ThrowIfNull(dbContextOptions);
        ArgumentNullException.ThrowIfNull(structureNodeValidator);

        _dbContextOptions = dbContextOptions;
        _structureNodeValidator = structureNodeValidator;
    }
}