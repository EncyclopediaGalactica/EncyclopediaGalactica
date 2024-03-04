namespace EncyclopediaGalactica.Services.Document.Service.Structure;

using Contracts.Input;
using Contracts.Output;
using Entities;
using FluentValidation;
using Interfaces.Structure;
using Mappers.Interfaces;
using Repository.Interfaces;
using Utils.GuardsService.Interfaces;
using ValidatorService;

public class AddNewStructureScenario : IAddNewStructureScenario
{
    private readonly IGuardsService _guardService;
    private readonly IStructureNodeMappers _structureNodeMappers;
    private readonly IStructureRepository _structureRepository;
    private readonly IValidator<StructureNode> _structureValidator;

    public AddNewStructureScenario(
        IStructureRepository structureRepository,
        IStructureNodeMappers structureNodeMappers,
        IValidator<StructureNode> structureValidator,
        IGuardsService guardsService)
    {
        ArgumentNullException.ThrowIfNull(structureRepository);
        ArgumentNullException.ThrowIfNull(structureNodeMappers);
        ArgumentNullException.ThrowIfNull(structureValidator);
        ArgumentNullException.ThrowIfNull(guardsService);

        _structureNodeMappers = structureNodeMappers;
        _guardService = guardsService;
        _structureValidator = structureValidator;
        _structureRepository = structureRepository;
    }

    public async Task<StructureNodeResult> AddNewAsync(
        long parentId,
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await AddNewBusinessLogicAsync(parentId, structureNodeInput)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<StructureNodeResult> AddNewBusinessLogicAsync(long parentId,
        StructureNodeInput structureNodeInput)
    {
        ValidateProvidedInput(parentId, structureNodeInput);
        StructureNode structureNode = _structureNodeMappers.MapStructureNodeInputToStructureNode(structureNodeInput);
        ValidateStructureEntity(structureNode);
        StructureNode newStructureNode = await _structureRepository.AddNewAsync(structureNode).ConfigureAwait(false);
        StructureNodeResult resultNode = _structureNodeMappers.MapStructureNodeToStructureNodeResult(newStructureNode);
        return resultNode;
    }

    private void ValidateStructureEntity(StructureNode structureNode)
    {
        _structureValidator.ValidateAsync(structureNode, o =>
        {
            o.IncludeRuleSets(Operations.Add);
            o.ThrowOnFailures();
        });
    }

    private void ValidateProvidedInput(long parentId, StructureNodeInput structureNodeInput)
    {
        _guardService.IsNotEqual(parentId, 0);
        _guardService.NotNull(structureNodeInput);
        _guardService.IsNotEqual(parentId, structureNodeInput.Id);
    }
}