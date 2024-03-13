namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Interfaces;

public class GetDocumentByIdContext : ISagaContextWithPayload<long>
{
    public long Payload { get; set; }
}