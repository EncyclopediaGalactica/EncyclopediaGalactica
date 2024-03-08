namespace EncyclopediaGalactica.Services.Document.Scenario.StructureNode;

using Contracts.Input;
using Contracts.Output;
using Entities;
using FluentValidation;
using Interfaces.StructureNode;
using Mappers.Interfaces;
using Repository.Interfaces;
using Utils.GuardsService.Interfaces;
using ValidatorService;

public class AddNewRootStructureNodeScenario : IAddNewRootStructureNodeScenario
{
    private readonly IGuardsService _guardService;
    private readonly IStructureNodeMappers _structureNodeMappers;
    private readonly IStructureNodeRepository _structureNodeRepository;
    private readonly IValidator<StructureNode> _structureValidator;

    public AddNewRootStructureNodeScenario(
        IStructureNodeRepository structureNodeRepository,
        IStructureNodeMappers structureNodeMappers,
        IValidator<StructureNode> structureValidator,
        IGuardsService guardsService)
    {
        ArgumentNullException.ThrowIfNull(structureNodeRepository);
        ArgumentNullException.ThrowIfNull(structureNodeMappers);
        ArgumentNullException.ThrowIfNull(structureValidator);
        ArgumentNullException.ThrowIfNull(guardsService);

        _structureNodeMappers = structureNodeMappers;
        _guardService = guardsService;
        _structureValidator = structureValidator;
        _structureNodeRepository = structureNodeRepository;
    }

    public async Task<StructureNodeResult> AddNewRootNodeAsync(StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await AddNewRootNodeBusinessLogicAsync(structureNodeInput)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<StructureNodeResult> AddNewRootNodeBusinessLogicAsync(StructureNodeInput structureNodeInput)
    {
        ValidateProvidedInput(structureNodeInput);
        StructureNode structureNode = _structureNodeMappers.MapStructureNodeInputToStructureNode(structureNodeInput);
        ValidateStructureEntity(structureNode);
        StructureNode newStructureNode =
            await _structureNodeRepository.AddNewAsync(structureNode).ConfigureAwait(false);
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

    private void ValidateProvidedInput(StructureNodeInput structureNodeInput)
    {
        _guardService.NotNull(structureNodeInput);
    }
}