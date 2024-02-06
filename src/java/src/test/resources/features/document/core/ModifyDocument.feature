Feature: Modify Document

  The Modify Document feature of the Encyclopedia Galactica backend core
  provides functionality to modify existing Document Entities in the system.
  The feature includes input validation.

  Scenario Outline: Modify Document input validation
    Given there is a Document entity
    And the name is 'name'
    And the description is 'description'
    And the Document is created
    And the name is change to '<name>'
    And the description is change to '<description>'
    When the Document is saved
    Then '<error>' message returned

    Examples:
      | name      | description | error                              |
      | <null>    | asd         | Modifying document process failed. |
      | <empty>   | asd         | Modifying document process failed. |
      | a         | asd         | Modifying document process failed. |
      | <3spaces> | asd         | Modifying document process failed. |
      | asd       | <null>      | Modifying document process failed. |
      | asd       | <empty>     | Modifying document process failed. |
      | asd       | a           | Modifying document process failed. |
      | asd       | <3spaces>   | Modifying document process failed. |

  Scenario: Modify Document
    Given there is a Document entity
    And the name is 'name'
    And the description is 'description'
    And the Document is created
    And the name is change to 'name2'
    And the description is change to 'description2'
    When the Document is saved
    Then the api returns the newly created Document
    And the name value is 'name2'
    And the description value is 'description2'
      
