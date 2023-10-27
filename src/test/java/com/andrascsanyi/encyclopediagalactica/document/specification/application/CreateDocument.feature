Feature: Add new Document implementation in the application

  Scenario Outline: Adding new Document throws exception when input is invalid
    When I want to create a new Document with the following data
      | id          | <id>          |
      | name        | <name>        |
      | description | <description> |
    Then the application throws <exception> exception

    Examples:
      | id | name             | description      | exception           |
      | 1  | name             | desc             | ValidationException |
      | 0  | <<null>>         | desc             | ValidationException |
      | 0  | <<empty>>        | desc             | ValidationException |
      | 0  | <<as>>           | desc             | ValidationException |
      | 0  | <<as >>          | desc             | ValidationException |
      | 0  | <<three_spaces>> | desc             | ValidationException |
      | 0  | name             | <<null>>         | ValidationException |
      | 0  | name             | <<empty>>        | ValidationException |
      | 0  | name             | <<as>>           | ValidationException |
      | 0  | name             | <<as >>          | ValidationException |
      | 0  | name             | <<three_spaces>> | ValidationException |

  Scenario: Adding new Document is successful
    When I want to create a new Document with the following data
      | id          | 0           |
      | name        | name        |
      | description | description |
    Then the application records the Document and returns with it

  Scenario: Adding new Document operation throws when document name uniqueness is violated
    When I want to create a new Document with the following data
      | id          | 0           |
      | name        | name        |
      | description | description |
    Then the application throws <<exception>> exception
    
    