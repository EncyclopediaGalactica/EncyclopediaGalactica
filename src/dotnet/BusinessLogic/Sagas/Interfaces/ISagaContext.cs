namespace EncyclopediaGalactica.BusinessLogic.Sagas.Interfaces;

public interface ISagaContext<T>
{
    T Payload { get; set; }
}