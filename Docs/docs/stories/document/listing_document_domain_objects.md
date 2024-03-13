# List Document domain objects

| Parameter           | Description                     |
|---------------------|---------------------------------|
| Goal                | List Document domain objects    |
| Secondary operation | None                            |
| Input               | None                            |
| Output              | List of Document domain objects |

There are no input validation rules.

# Sequence diagram

```mermaid
sequenceDiagram
    UI -->> GQL: requests list of Documents
    activate GQL
    GQL -->> GetDocumentsSaga: executes
    activate GetDocumentsSaga
    GetDocumentsSaga -->> GetDocumentsCommand: executes
    activate GetDocumentsCommand
    GetDocumentsCommand -->> GetDocumentsSaga: list of Documents
    deactivate GetDocumentsCommand
    GetDocumentsSaga -->> GQL: list of Documents
    deactivate GetDocumentsSaga
    GQL -->> UI: list of Documents
    deactivate GQL
```