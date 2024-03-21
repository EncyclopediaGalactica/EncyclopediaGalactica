# Domain objects

```mermaid
erDiagram
    Document || -- || StructureNode: contains
    StructureNode }o -- o{ Relation: contains
```