namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Contracts;
using Interfaces;

public class AddDocumentSagaContext : ISagaContextWithPayload<DocumentInput>
{
    public DocumentInput Payload { get; set; }
}