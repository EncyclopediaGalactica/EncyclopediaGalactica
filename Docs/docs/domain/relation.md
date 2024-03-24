# Relation

A Relation is about a relationship between two [Document](document.md)s.
A Relation does not only store the fact that two Documents are related in some ways, but also
provides information about the quality of the relation.

## Relation Entity

| Field    | Description                |
|----------|----------------------------|
| Id       | Unique identifier          |
| LeftEnd  | Left side of the relation  |
| RightEnd | Right side of the relation |

## Operations

### Create a new Relation

A new Relation is created when two Documents need to be connected.

#### Validation rules

| Rule                                 | Result    |
|--------------------------------------|-----------|
| Id must be `zero`                    | Exception |
| LeftEnd must be greater than `zero`  | Exception |
| RightEnd must be greater than `zero` | Exception |

### Edit a Relation

Edit Relation happens when an already existing Relation details need to be changed.

#### Validation rules

| Rule                                 | Result    |
|--------------------------------------|-----------|
| Id must not be `zero`                | Exception |
| LeftEnd must be greater than `zero`  | Exception |
| RightEnd must be greater than `zero` | Exception |

### Delete a Relation

Delete Relation happen when an already existing Relation need to be deleted.

#### Validation rules

| Rule                                 | Result    |
|--------------------------------------|-----------|
| Id must not be `zero`                | Exception |

### List Relations

