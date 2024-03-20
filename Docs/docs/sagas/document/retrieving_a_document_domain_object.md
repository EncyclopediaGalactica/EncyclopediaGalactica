# Retrieve a Document domain object

| Parameter           | Description                       |
|---------------------|-----------------------------------|
| Goal                | Retrieve a Document domain object |
| Secondary operation | None                              |
| Input               | Document Id                       |
| Output              | None                              |

# Input validation rules

| Field       | Rule                           | Action    |
|-------------|--------------------------------|-----------|
| Id          | Must not be `zero`             | Exception |

# Sequence diagram

```mermaid
sequenceDiagram
    UI -->> GQL: posts details of the new Document
    activate GQL
    GQL -->> GetDocumentByIdSaga: details of the new Document
    activate GetDocumentByIdSaga
    GetDocumentByIdSaga -->> GetDocumentByIdCommand: details of the new Document
    activate GetDocumentByIdCommand
    GetDocumentByIdCommand -->> GetDocumentByIdSaga: returns the Id of the Document
    deactivate GetDocumentByIdCommand
    GetDocumentByIdSaga -->> GQL: returns Document
    deactivate GetDocumentByIdSaga
    GQL -->> UI: returns Document
    deactivate GQL
```