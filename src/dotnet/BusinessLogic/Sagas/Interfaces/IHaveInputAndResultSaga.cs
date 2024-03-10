namespace EncyclopediaGalactica.BusinessLogic.Sagas.Interfaces;

/// <summary>
///     Saga is responsible for orchestration of commands in order to achieve a goal.
/// </summary>
/// <typeparam name="TReturnType">The return type when the saga finishes.</typeparam>
/// <typeparam name="TPayloadType">The payload for the saga.</typeparam>
public interface IHaveInputAndResultSaga<TReturnType, TPayloadType> : ISaga
{
    /// <summary>
    ///     Executes the saga
    /// </summary>
    /// <param name="context">Context including the <see cref="TPayloadType" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>
    ///     <see cref="TReturnType" />
    /// </returns>
    Task<TReturnType> ExecuteAsync(TPayloadType context,
        CancellationToken cancellationToken = default);
}