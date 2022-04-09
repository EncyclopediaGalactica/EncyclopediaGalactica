Feature: Source Format Node Api feature
As a data curator
I need to be able to manage the Source Format Nodes in the system
By using its CRUD operations available on its Api
    
    Scenario Outline: API returns Http status code 400 when input is invalid
        Given there is the following endpoint
        | url                  |
        | api/sourceformatnode |
        And there is the operation endpoint
        | url |
        | add |
        And the following SourceFormatNode data
        | Name   |
        | <name> |
        When SourceFormatNode is sent to endpoint
        Then the api returns the following http status code
        
    Examples: 
    | name        |
    | null        |
    | emptystring |
    | bb          |
    | threespaces |