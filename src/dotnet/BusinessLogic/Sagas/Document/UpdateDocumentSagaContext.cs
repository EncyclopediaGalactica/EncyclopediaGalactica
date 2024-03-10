namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Interfaces;

public class UpdateDocumentSagaContext : ISagaContext<DocumentInput>
{
    public DocumentInput Payload { get; set; }
}