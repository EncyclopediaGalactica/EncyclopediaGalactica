namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Mappers;
using Validators;

public class AddStructureNodeTreeCommand : IAddStructureNodeTreeCommand
{
    private readonly IGuardsService _guardService;
    private readonly IStructureNodeMappers _structureNodeMappers;
    private readonly IStructureNodeRepository _structureNodeRepository;
    private readonly IValidator<StructureNode> _structureValidator;

    public AddStructureNodeTreeCommand(
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

    public async Task AddTreeAsync(StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await AddNewRootNodeBusinessLogicAsync(structureNodeInput)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task AddNewRootNodeBusinessLogicAsync(StructureNodeInput structureNodeInput)
    {
        ValidateProvidedInput(structureNodeInput);
        StructureNode structureNode = _structureNodeMappers.MapStructureNodeInputToStructureNode(structureNodeInput);
        ValidateStructureEntity(structureNode);
        StructureNode newStructureNode =
            await _structureNodeRepository.AddNewAsync(structureNode).ConfigureAwait(false);
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