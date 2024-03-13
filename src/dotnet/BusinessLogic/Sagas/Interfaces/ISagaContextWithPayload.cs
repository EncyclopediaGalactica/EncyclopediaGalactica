namespace EncyclopediaGalactica.BusinessLogic.Sagas.Interfaces;

public interface ISagaContextWithPayload<T> : ISagaContext
{
    T Payload { get; set; }
}