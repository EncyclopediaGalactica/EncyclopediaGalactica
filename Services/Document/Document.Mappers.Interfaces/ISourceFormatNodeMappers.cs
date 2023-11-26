namespace EncyclopediaGalactica.Services.Document.Mappers.Interfaces;

using Contracts.Input;
using Entities;

public interface ISourceFormatNodeMappers
{
    /// <summary>
    ///     Maps <see cref="SourceFormatNodeInput" /> to a <see cref="SourceFormatNode" />
    /// </summary>
    /// <param name="input">The dto to be mapped</param>
    /// <exception cref="ArgumentNullException">
    ///     Input parameter is null
    /// </exception>
    /// <returns>
    ///     Returns a <see cref="SourceFormatNode" /> object
    /// </returns>
    SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeInput input);

    /// <summary>
    ///     Maps a <see cref="SourceFormatNode" /> to a <see cref="SourceFormatNodeInput" />.
    ///     It does in the way the <see cref="SourceFormatNode" /> relations are not carried through, meaning
    ///     navigation properties are not populated.
    /// </summary>
    /// <param name="node">The <see cref="SourceFormatNodeInput" /> elem going to be mapped</param>
    /// <exception cref="ArgumentNullException">
    ///     Input parameter is null
    /// </exception>
    /// <returns>
    ///     Returns a <see cref="SourceFormatNodeInput" /> which property values equal to the original one.
    /// </returns>
    SourceFormatNodeInput MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(SourceFormatNode node);

    /// <summary>
    ///     Maps <see cref="SourceFormatNode" /> elements of a list to <see cref="SourceFormatNodeInput" />.
    ///     Does the mapping in the way the relations are not carried through,
    ///     meaning the navigation properties are not populated.
    /// </summary>
    /// <param name="sourceFormatNodes">The list of <see cref="SourceFormatNodeInput" /></param>
    /// <returns>
    ///     <see cref="List{T}" /> representing the result of operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Input parameter is null
    /// </exception>
    List<SourceFormatNodeInput> MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(
        List<SourceFormatNode> sourceFormatNodes);

    /// <summary>
    ///     Maps the provided <see cref="SourceFormatNode" /> object to a <see cref="SourceFormatNodeInput" />.
    ///     The mapping includes all properties, even those which has a list or tree structure.
    /// </summary>
    /// <param name="sourceFormatNode">The object should be mapped</param>
    /// <returns>
    ///     Returns a <see cref="SourceFormatNodeInput" /> object where its property values are equal to the original,
    ///     provided object properties
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Input parameter is null
    /// </exception>
    SourceFormatNodeInput MapSourceFormatNodeToSourceFormatNodeDto(SourceFormatNode sourceFormatNode);
}