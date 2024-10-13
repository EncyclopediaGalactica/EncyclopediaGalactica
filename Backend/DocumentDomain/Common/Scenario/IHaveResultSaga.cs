namespace EncyclopediaGalactica.DocumentDomain.Common.Scenario;

using LanguageExt;

/// <summary>
///     Saga is responsible for orchestration of commands in order to achieve a goal.
/// </summary>
/// <typeparam name="TReturnType">The return type when the saga finishes.</typeparam>
/// <typeparam name="TPayloadType">The payload for the saga.</typeparam>
public interface IHaveResultSaga<TReturnType> : ISaga
{
    /// <summary>
    ///     Executes the saga
    /// </summary>
    /// <param name="context">Context including the <see cref="TPayloadType" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>
    ///     <see cref="TReturnType" />
    /// </returns>
    Task<Option<TReturnType>> ExecuteAsync(ISagaContext context, CancellationToken cancellationToken = default);
}