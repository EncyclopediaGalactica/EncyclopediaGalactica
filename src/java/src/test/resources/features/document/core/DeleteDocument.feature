Feature: Delete Document

  The Delete Document feature of the Encyclopedia Galactica backend core
  provides functionality to delete existing Document Entities from the system.
  The feature includes input validation.

  Scenario: Delete Document input validation
    When the Document with id 0 is deleted
    Then 'Deleting document process failed.' message returned

  Scenario: Delete Document
    Given there is a Document entity
    And the name is 'name'
    And the description is 'description'
    And the Document is created
    When the Document is deleted
    Then the api returns no error
      
