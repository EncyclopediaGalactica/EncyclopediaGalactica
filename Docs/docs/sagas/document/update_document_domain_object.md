# Update a Document domain object

| Parameter           | Description                                                      |
|---------------------|------------------------------------------------------------------|
| Goal                | Update a new Document domain object                              |
| Secondary operation | Update StructureNode tree                                        |
| Input               | A DocumentInput where the properties already have the new values |
| Output              | The changed Document domain object                               |

# Input validation rules

| Field       | Rule                           | Action    |
|-------------|--------------------------------|-----------|
| Id          | Must not be `zero`             | Exception |
| Name        | Must not be `null`             | Exception |
| Name        | Must not be `empty`            | Exception |
| Name        | Must be longer than 3 chars    | Exception |
| Name        | Must be shorter than 255 chars | Exception |
| Description | Must not be `null`             | Exception |
| Description | Must not be `empty`            | Exception |
| Description | Must be longer than 3 chars    | Exception |
| Description | Must be shorter than 255 chars | Exception |

# Secondary operations

## Update the StructureNode tree

Until there are no items connecting to a Structure Node the update Document saga will delete and 
re-add the StructureNode tree for a Document.

# Sequence diagram

```mermaid
sequenceDiagram
    UI -->> GQL: posts DocumentInput
    activate GQL
    GQL -->> UpdateDocumentSaga: executes
    activate UpdateDocumentSaga
    UpdateDocumentSaga -->> UpdateDocumentCommand: passes DocumentInput
    activate UpdateDocumentCommand
    UpdateDocumentCommand -->> UpdateDocumentSaga: returns the Id of the Document
    deactivate UpdateDocumentCommand
    UpdateDocumentSaga -->> UpdateStructureNodeTreeCommand: passes DocumentInput
    activate UpdateStructureNodeTreeCommand
    UpdateStructureNodeTreeCommand -->> UpdateDocumentSaga: void
    deactivate UpdateStructureNodeTreeCommand
    UpdateDocumentSaga -->> GetDocumentById: requests Document by Id
    activate GetDocumentById
    GetDocumentById -->> UpdateDocumentSaga: returns Document
    deactivate GetDocumentById
    UpdateDocumentSaga -->> GQL: returns Document
    deactivate UpdateDocumentSaga
    GQL -->> UI: returns Document
    deactivate GQL
```