namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Interfaces;

public class DeleteDocumentSagaContext : ISagaContextWithPayload<Int64>
{
    public Int64 Payload { get; set; }
}