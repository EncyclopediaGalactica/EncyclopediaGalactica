# Code Generator Specification based on Configuration file

The Rest Api Client Generator follows the Api first approach where the steps looks like
the following:

- define the Api
- generate the code
- test the result
- make a decision whether further changes are needed

The corner stones of the process are

- the generator itself,
- the generator configuration file
- OpenApi file in yaml format

The above process generates the following codes:

- Data Transfer Objects: it represents the contract of the Api
- Data Transfer Object Tests: these tests are checking if any logic got into the Data Transfer
  Objects
- Controller: the controllers contains the smallest possible code which is in this case just
  calling the business logic

## The generator internal data structure

The generator considers the following structural levels:

- **solution** - this is a directory 1 level above the projects and owns all projects
- **project** - dedicated directories for a purpose, for example Dto, Dto Tests have their own
  directory. A project must only have a single solution as parent.
- **directory** - plain directory on the file system. A directory must have a single project as
  parent.

The above structure is also represented by the generators internal data structure.

### SolutionInfo

It contains all the Solution related information such as:

- original values coming from configuration file
- values going to be used, these are the transformed original values
- list of projects

### ProjectInfo

It contains all the Project related information such as:

- original values from configuration file
- values going to be used, these are basically transformed original values
- list of types

### TypeInfo

It contains all the particular Type related info such as:

- original values from configuration file
- values going to be used, these are basically transformed original values
- list of dependencies (other types, usings)
- parents if there is any

### VariableInfo

It contains all the particular Variable related info such as:

- original name
- transformed name
- dependencies which can be a framework level (List<>) or other type level and manifested in
  using format

## c-sharp

In case of c-sharp the code generator considers a solution where the following projects exist.
The generator expects the solution in a certain format, especially directory structure. This
one is demonstrated below.

### Solution level rules

- Solution Name - the name of the solution and this value defines the name of the solution
  file. This value is specified like: `solution_name`.
- Target directory - the directory where the solution is located. This value can be specified
  as absolute or relative path using the `target_directory` key.

### Dto Project level configurations

- Dto project in the `${target_directory}/{$solution_name}.Dto` directory
- Dto project file in the `${target_directory}/{$solution_name}.Dto/` directory with the
  name `{$solution_name}.Dto.csproj`
- Dto porject namespace is `${solution_base_namespace}.${dto_project_namespace}` where
  the `${dto_project_namespace}` default value is **dto**. However, it can be overwritten by
  providing the new value in the configuration file `dto_project_namespace`, but as a result
  the namespace structure won't honor the directory structure

### Dto Unit Test Project

- Dto Unit Tests project in the `${target_directory}/{$solution_name}.Dto.Tests.Unit` directory
- Dto Unit Tests project file in the `${target_directory}/{$solution_name}.Dto.Tests.Unit`
  directory with the name `{$solution_name}.Dto.Tests.Unit.csproj`

# Configuration via json file

## $schema

```json
{
  "$schema": "https://something.something/schema.json"
}
```

It defines the location of the schema file.

## lang

```json
{
  "lang": "csharp"
}
```

It defines what language will be generated.

The code generator based on this value will instantiate the necessary code generator codes.

If this value is not provided the code generation stops with an error message.

## OpenApi Specification Path

```json
{
  "openapi_specification_path": "absolute or relative path to the openapi.yaml file"
}
```

The generator uses the OpenApi file to generate majority of the codebase.

When this file is missing or the parameter contains invalid value the code generation stops
with and error.

## Target directory

```json
{
  "target_directory": "absolute or relative path where the solution is located"
}
```

The place where the solution file with the name of `solution_name` is located. This is the base
where from the expected directory structure will be either build up or followed.

The directory can be defined both relative and absolute path way. The code generator can handle
both.

If the directory does not exist the generator execution stops with an error.

## Solution Base Namespace

```json
{
  "solution_base_namespace": "something.namespace"
}
```

The solution level base namespace is used to build other namespaces during generation.

Validation and transformation rules are the following:

- "something.namespace" ==> "Something.Namespace"
- "something." ==> "Something"
- ".something" ==> "Something"
- null ==> throw
- string.emtpy ==> throw
- "  " ==> throw

## Solution Name

```json
{
  "solution_name": "SolutionName"
}
```

The name of the solution. The code generator will look for a file with this name with the
configured file type.

### Errors

- when there is no solution file in the defined directory
- no solution name is defined
- solution name is space(s)
- solution name starts with a non letter character
- solution name contains other than alphanumerical and dot character(s)

### C# specifics

