namespace EncyclopediaGalactica.Services.Document.Service.Structure;

using Contracts.Input;
using Entities;
using FluentValidation;
using Interfaces.Structure;
using Mappers.Interfaces;
using Repository.Interfaces;
using Utils.GuardsService.Interfaces;
using ValidatorService;

public class AddNewStructureCommand : IAddNewStructureCommand
{
    private readonly IGuardsService _guardService;
    private readonly IStructureMappers _structureMappers;
    private readonly IStructureRepository _structureRepository;
    private readonly IValidator<Structure> _structureValidator;

    public AddNewStructureCommand(
        IStructureRepository structureRepository,
        IStructureMappers structureMappers,
        IValidator<Structure> structureValidator,
        IGuardsService guardsService)
    {
        ArgumentNullException.ThrowIfNull(structureRepository);
        ArgumentNullException.ThrowIfNull(structureMappers);
        ArgumentNullException.ThrowIfNull(structureValidator);
        ArgumentNullException.ThrowIfNull(guardsService);

        _structureMappers = structureMappers;
        _guardService = guardsService;
        _structureValidator = structureValidator;
        _structureRepository = structureRepository;
    }

    public async Task<StructureInput> AddNewAsync(
        long parentId,
        StructureInput structureInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await AddNewBusinessLogicAsync(parentId, structureInput)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<StructureInput> AddNewBusinessLogicAsync(long parentId,
        StructureInput structureInput)
    {
        ValidateProvidedInput(parentId, structureInput);
        Structure structure = _structureMappers.MapStructureDtoToStructure(structureInput);
        ValidateStructureEntity(structure);
        Structure newStructure = await _structureRepository.AddNewAsync(structure).ConfigureAwait(false);
        StructureInput resultInput = _structureMappers.MapStructureToStructureDto(newStructure);
        return resultInput;
    }

    private void ValidateStructureEntity(Structure structure)
    {
        _structureValidator.ValidateAsync(structure, o =>
        {
            o.IncludeRuleSets(Operations.Add);
            o.ThrowOnFailures();
        });
    }

    private void ValidateProvidedInput(long parentId, StructureInput structureInput)
    {
        _guardService.IsNotEqual(parentId, 0);
        _guardService.NotNull(structureInput);
        _guardService.IsNotEqual(parentId, structureInput.Id);
    }
}