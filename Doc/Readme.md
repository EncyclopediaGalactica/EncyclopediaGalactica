# Encyclopedia Galactica

The goal of this application is being able to process high volume of information. The user
should be able to
create connections between piece of informations. The application should be able to help this.

In more technical details the above mission looks like the following:

- ability to load data into the system in any format (let's say with some limitations)
- ability to initiate search in the stored documents easily
- ability to create abstract information structures (tree, graph, thesaurus, taxonomy) and
  chain the piece of information to this
- the software should be able to offer categorisation and catalogisation based on provided
  metadata about the loaded doc

## Loading Data

In this case the format can be anything which is not a binary format, e.g. RTF, TXT, RSS, Open
Office format, you name it. In these cases the system knows and understands these formats and
can turn them into the system format, which is an SQL structure. The system makes possible to
build a document structure and eventually process an input format.

## Searching in the stored data

The system can search for words and sentences. The system builds its internal trie structures
helping search.

The other way of making a search in the system is along the discipline structures described in
a thesaurus (in reality it is a graph).

## Create abstract information structures

As I mentioned previously the system offers functionality to build abstract information
structures. Based on these structures input information processing can be executed, input
format forms (basically forms which represent the information structure on the UI) created.

## Automatic, suggested categorisation and catalogisation

Knowing the discipline structures, stored in a thessaurus, the input information (for example,
a text file) can have automatic catalog information. The system can suggest these by meta data
or content.

# How the system processes input data?

## The document

A [Document](Document/Readme.md) to be processed has a lot of properties, you may call it meta
data, which might define how the processing happens. For example:

- Type
- Source
- Process Schedule
- Enabled/Disabled
- SourceFormat

## Source information

The [Source]() describes the source of the input files. It can be an URL or "upload" if it is
uploaded.

## Document structures

The system should know the structure of the input data. It can be any text based format like
XML, RSS, ODT or RTF, or whatever else. This structure is described
by [SourceFormatNodes](SourceFormats/Readme.md).

