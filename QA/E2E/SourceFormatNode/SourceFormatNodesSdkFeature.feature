Feature: Source Format Node SDK feature
As a Data Curator
I need to be able to manage Source Format Nodes in the system using
For using the system programmatically by its SDK package.

    Scenario Outline: SDK validates input data and throws exception it is invalid
        Given there is the 'source_format' SDK providing 'add_new_sourceformatnode' functionality
        And the 'name' string parameter value is '<name>'
        When I prepare the data to be sent
        Then the SDK throws 'SdkModelsException'

        Examples:
          | name        |
          | emptystring |
          | null        |
          | 2chars      |
          | 3spaces     |
          
Scenario Outline: It is possible to create Source Format Node entities using SDK
    Given there is the 'source_format' SDK providing 'add_new_sourceformatnode' functionality
    And the 'name' string parameter value is 'asd'
    And I prepare and store the data to be sent
    When I send the data using 'source_format' SDK
    Then I get an response
    And the sdk response contains the 'result' property
    And sdk response 'result' property is 'not_null'
    And the sdk response contains the 'httpstatuscode' property
    And sdk response 'httpstatuscode' property equals to '201'
    And the sdk response contains the 'isoperationsuccessful' property
    And sdk response 'isoperationsuccessful' property equals to 'true'
#    And the newly created 'SourceFormatNode' entity id value is 'greater_than_zero'
    