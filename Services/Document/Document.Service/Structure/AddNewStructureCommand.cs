namespace EncyclopediaGalactica.Services.Document.Service.Structure;

using Dtos;
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

    public async Task<StructureDto> AddNewAsync(
        long parentId,
        StructureDto structureDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await AddNewBusinessLogicAsync(parentId, structureDto)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<StructureDto> AddNewBusinessLogicAsync(long parentId, StructureDto structureDto)
    {
        ValidateProvidedInput(parentId, structureDto);
        Structure structure = _structureMappers.MapStructureDtoToStructure(structureDto);
        ValidateStructureEntity(structure);
        Structure newStructure = _structureRepository.AddNewAsync(structure).ConfigureAwait(false);
        StructureDto resultDto = _structureMappers.MapStructureToStructureDto(newStructure);
        return resultDto;
    }

    private void ValidateStructureEntity(Structure structure)
    {
        _structureValidator.ValidateAsync(structure, o =>
        {
            o.IncludeRuleSets(Operations.Add);
            o.ThrowOnFailures();
        });
    }

    private void ValidateProvidedInput(long parentId, StructureDto structureDto)
    {
        _guardService.IsNotEqual(parentId, 0);
        _guardService.NotNull(structureDto);
        _guardService.IsNotEqual(parentId, structureDto.Id);
    }
}