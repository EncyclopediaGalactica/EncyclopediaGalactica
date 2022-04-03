Feature: Source Format Node feature
As a data curator
I need to be able to create structures describing data formats
in this process managing SourceFormatNodes is essential
So, I need CRUD and other tree related functionalities available via API.
    
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