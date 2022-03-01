namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;

public partial class SourceFormatNodeRepository
{
    public async Task<SourceFormatNode> AddAsync(SourceFormatNode node)
    {
        try
        {
            _ctx.SourceFormatNodes.Add(node);
            await _ctx.SaveChangesAsync().ConfigureAwait(false);
            return node;
        }
        catch (Exception e)
        {
            string msg = $"Exception was thrown while {nameof(SourceFormatNodeRepository)}.{nameof(AddAsync)} " +
                         $"was executed. For further information see inner exception.";
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }
}