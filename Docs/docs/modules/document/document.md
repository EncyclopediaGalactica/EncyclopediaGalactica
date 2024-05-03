# Document module

The document module is about managing the documents in the system.
The document in the previous sentence means a container including data.
For managing the container and its structure the Document module provides separate grids
and dialogs.
For managing the data the module provides a rich interface.

The idea behind managing the container details is that the way the data structured also carries
valuable information.
With the system we want to have the ability to create and recreate these structures.
When it comes to structures think of parts, chapters and paragraphs when a book is discussed.
In case RSS the structure is described an XML file.
All this information needd to be stored in the system to have the ability to deconstruct these 
in a way their meaning still retained.

From a more theoretical data structure management point of view we can say that Document is
the entity while FileType, RelationType and so on are the value objects around it.
It is possible that a value object later became an entity.
This depends on how the abstraction of a document and its data evolves in the system.

For managing the Document entity and its value objects the system provides the following grids:

- [Applications Grid](ui/application_grid.md)
- [Relation Type Grid](ui/relation_type_grid.md)
- [File Format Grid](ui/file_format.md)
- Document Type Grid
- Document Structures Grid
- [Relation Grid](ui/relation_grid.md)

The other mission of the module is providing a rich interface adding data to the structures
stored and managed in the system.
It is like editing a document.

To support editing data content of a document the module provides the following grids and user
interface:

- Document Catalog Grid
- Relation Grid