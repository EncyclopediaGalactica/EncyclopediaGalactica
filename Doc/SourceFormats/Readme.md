# Encyclopedia Galactica
The goal of this application is being able to process high volume of information. The user should be able to 
create connections between piece of informations. The application should be able to help this.

In more technical details the above mission looks like the following:

- ability to load data into the system in any format (let's say with some limitations)
- ability to initiate search in the stored documents easily
- ability to create abstract information structures (tree, graph, thesaurus, taxonomy) and chain the piece of information to this
- the software should be able to offer categorisation and catalogisation based on provided metadata about the loaded doc

## Loading Data

In this case the format can be anything which is not a binary format, e.g. RTF, TXT, RSS, Open Office format, you name it. In these cases the system knows and understands these formats and can turn them into the system format, which is an SQL structure.
