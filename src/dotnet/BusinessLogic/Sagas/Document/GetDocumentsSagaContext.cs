namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Interfaces;

public class GetDocumentsSagaContext : ISagaContext<DocumentInput>
{
    public DocumentInput Payload { get; set; }
}