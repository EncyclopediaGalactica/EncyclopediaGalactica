Feature: Create Document

  The Create Document feature of the Encyclopedia Galactica backend core
  provides functionality to create new Document Entities in the system.
  The feature includes input validation.

  Scenario Outline: Create Document input validation
    Given there is a Document entity
    And the id is <id>
    And the name is '<name>'
    And the description is '<description>'
    When the Document is created
    Then '<error>' message returned

    Examples:
      | id | name      | description | error                            |
      | 1  | asd       | asd         | Create scenarios process failed. |
      | 0  | <null>    | asd         | Create scenarios process failed. |
      | 0  | <empty>   | asd         | Create scenarios process failed. |
      | 0  | a         | asd         | Create scenarios process failed. |
      | 0  | <3spaces> | asd         | Create scenarios process failed. |
      | 0  | asd       | <null>      | Create scenarios process failed. |
      | 0  | asd       | <empty>     | Create scenarios process failed. |
      | 0  | asd       | a           | Create scenarios process failed. |
      | 0  | asd       | <3spaces>   | Create scenarios process failed. |

  Scenario Outline: Create Document
    Given there is a Document entity
    And the id is <id>
    And the name is '<name>'
    And the description is '<description>'
    When the Document is created
    Then the api returns the newly created Document
    And the name value is '<name>'
    And the description value is '<description>'
    And the id value is greater than 0

    Examples:
      | id | name      | description         |
      | 0  | name      | some description    |
      | 0  | name name | further description |
      
