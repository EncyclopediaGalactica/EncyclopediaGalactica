# Create a new Document domain object

| Parameter           | Description                               |
|---------------------|-------------------------------------------|
| Goal                | Create a new Document domain object       |
| Secondary operation | None                                      |
| Input               | Details of the new Document domain object |
| Output              | The new Document domain object            |

# Input validation rules

| Field       | Rule                           | Action    |
|-------------|--------------------------------|-----------|
| Id          | Must be zero                   | Exception |
| Name        | Must not be `null`             | Exception |
| Name        | Must not be `empty`            | Exception |
| Name        | Must be longer than 3 chars    | Exception |
| Name        | Must be shorter than 255 chars | Exception |
| Description | Must not be `null`             | Exception |
| Description | Must not be `empty`            | Exception |
| Description | Must be longer than 3 chars    | Exception |
| Description | Must be shorter than 255 chars | Exception |


# Sequence diagram

```mermaid
sequenceDiagram
    UI -->> GQL: details of the new Document
    activate GQL
    GQL -->> CreateNewDocumentSaga: details of the new Document
    activate CreateNewDocumentSaga
    CreateNewDocumentSaga -->> CreateNewDocumentCommand: details of the new Document
    activate CreateNewDocumentCommand
    CreateNewDocumentCommand -->> CreateNewDocumentSaga: returns the Id of the Document
    deactivate CreateNewDocumentCommand
    CreateNewDocumentSaga -->> GetDocumentById: requests Document by Id
    activate GetDocumentById
    GetDocumentById -->> CreateNewDocumentSaga: returns Document
    deactivate GetDocumentById
    CreateNewDocumentSaga -->> GQL: returns Document
    deactivate CreateNewDocumentSaga
    GQL -->> UI: returns Document
    deactivate GQL
```
