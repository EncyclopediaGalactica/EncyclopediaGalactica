Feature: Source Format Node SDK feature
As a Data Curator
I need to be able to manage Source Format Nodes in the system using
For using the system programmatically by its SDK package.

    Scenario Outline: SDK validates input data and throws exception it is invalid
        Given there is the Source Format SDK providing 'add_new_sourceformatnode' functionality
        And the 'name' string parameter value is '<name>'
        When I prepare the data to be sent
        Then the SDK throws 'SdkModelsException'

        Examples:
          | name        |
          | emptystring |
          | null        |
          | 2chars      |
          | 3spaces     |