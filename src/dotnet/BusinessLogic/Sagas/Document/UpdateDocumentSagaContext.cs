namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Contracts;
using Interfaces;

public class UpdateDocumentSagaContext : ISagaContextWithPayload<DocumentInput>
{
    public DocumentInput Payload { get; set; }
}