# Delete Document domain object

| Parameter           | Description                                                 |
|---------------------|-------------------------------------------------------------|
| Goal                | Delete a Document domain object                             |
| Secondary operation | Clean up all accompanied structures like StructureNode tree |
| Input               | Document Id                                                 |
| Output              | None                                                        |

# Input validation rules

| Field       | Rule                           | Action    |
|-------------|--------------------------------|-----------|
| Id          | Must not be `zero`             | Exception |

# Sequence diagram

```mermaid
sequenceDiagram
    UI -->> GQL: posts details of the new Document
    activate GQL
    GQL -->> DeleteDocumentSaga: details of the new Document
    activate DeleteDocumentSaga
    DeleteDocumentSaga -->> DeleteDocumentCommand: details of the new Document
    activate DeleteDocumentCommand
    DeleteDocumentCommand -->> DeleteDocumentSaga: returns the Id of the Document
    deactivate DeleteDocumentCommand
    DeleteDocumentSaga -->> GQL: returns Document
    deactivate DeleteDocumentSaga
    GQL -->> UI: returns Document
    deactivate GQL
```
