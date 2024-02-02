# Rest Api SDK Generator
This beast is a tool designed for Encyclopedia Galactica infrastructure to generate
the majority of the boilerplate code.

# Current version
0.2.0

# Supported languages
- C# (.NET Core)

# The workflow
- Encyclopedia Galactica consists of many microservices, basically Rest endpoints
- these services have their own OpenApi specification in yaml format
- whatever possible to generate from the OpenApi yaml it will be generated

## New endpoint
- define the Rest endpoint in Open Api yaml format
- define the configuration file for the endpoint
- create the solution file (.sln file)
- create the project directories (ask the template from the generator)
- create the project files (.csproj file)
- run the generator

## Change in the endpoint
- make the changes in the Open Api definition
- align the configuration values
- align the project directory structure
- align the project structure
- run the generator

# Structure
We need an example service
- `Example.sln`
- `Dto/Example.Dto.csproj`
- `Dto/{AdditionalPath}` -- additional path is defined in the configuration file
- `Dto.Tests.Unit/Example.Dto.Tests.Unit.csproj`
- `Dto.Tests.Unit/{AdditionalPath}` -- additional path is defined in the configuration file