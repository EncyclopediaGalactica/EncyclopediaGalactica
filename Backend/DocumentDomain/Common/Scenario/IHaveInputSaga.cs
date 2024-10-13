namespace EncyclopediaGalactica.DocumentDomain.Common.Scenario;

/// <summary>
///     Saga is responsible for orchestration of commands in order to achieve a goal.
/// </summary>
/// <typeparam name="TPayloadType">The payload for the saga.</typeparam>
public interface IHaveInputSaga<TPayloadType> : ISaga
{
    /// <summary>
    ///     Executes the saga
    /// </summary>
    /// <param name="context">Context including the <see cref="TPayloadType" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task ExecuteAsync(TPayloadType context,
        CancellationToken cancellationToken = default);
}