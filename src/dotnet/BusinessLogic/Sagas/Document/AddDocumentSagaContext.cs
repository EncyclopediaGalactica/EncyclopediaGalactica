namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Interfaces;

public class AddDocumentSagaContext : ISagaContext<DocumentInput>
{
    public DocumentInput Payload { get; set; }
}