namespace EncyclopediaGalactica.DocumentDomain.Common.Commands;

using LanguageExt;

public interface IHaveResultCommand<TResult>
{
    Task<Option<TResult>> ExecuteAsync(CancellationToken cancellationToken);
}