# Source Formats domain
The purpose of Source Format domain is describing a document structure. The structure 
itself consists of a nodes describing only the structure and facilitating points where 
additional structural property objects can be added.

For example, every node has a Type value:

- `text` type means that the node contains textual information
- `group` type means that the tree underneath the given node belongs to the group node

## SourceFormatNode
SourceFormatNodes describe a tree basis of a structure.