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

## c-sharp

In case of c-sharp the code generator considers a solution where the following projects exist.
The generator expects the solution in a certain format, especially directory structure. This
one is demonstrated below.

- Solution Name - the name of the solution and this value defines the name of the solution
  file. This value is specified like: `solution_name`.
- Target directory - the directory where the solution is located. This value can be specified
  as absolute or relative path using the `target_directory` key.
- Dto project in the `${target_directory}/{$solution_name}.Dto` directory
- Dto project file in the `${target_directory}/{$solution_name}.Dto/` directory with the
  name `{$solution_name}.Dto.csproj`
- Dto Unit Tests project in the `${target_directory}/{$solution_name}.Dto.Tests.Unit` directory
- Dto Unit Tests project file in the `${target_directory}/{$solution_name}.Dto.Tests.Unit`
  directory with the name `{$solution_name}.Dto.Tests.Unit.csproj`

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

## Dto Project Name

```json
{
  "dto_project_name": "Dto.Project.Name21"
}
```

The Dto Project Name defines the name of the Dto project. The Dto project (Data Transfer
Object) contains the objects representing the contract to the outside. These objects are
generated based on the schema definitions in the Open Api file.

### Validation

| Input           | Validation Result |
|-----------------|-------------------|
| null            | throw             |
| string.empty    | throw             |
| "{space(s)}   " | throw             |

### C# Specifics

The generator has a theoretical inner structure for a C# project. The Dto project is located at
the `{$TargetDirectory}/{$SolutionName}.Dto` directory.

#### Validation and Transformation

The table below inherits all items from the validation rules above

Example for a solution project name: `Something.Project.Name32`

| Input                                                                    | Transformation or Validation result |
|--------------------------------------------------------------------------|-------------------------------------|
| Alphanumerical characters and Dot                                        | accept                              |
| More than Alphanumerical characters and Dot                              | throws                              |
| Name starts **not** with a letter                                        | throw                               |
| Name contains dot and the first char after the dot is not letter         | throw                               |
| Name contains dot and the first char after is a letter but not uppercase | transform to uppercase              |
| Name starts with lowercase character                                     | transform to uppercase              |
| Chars after the dot(s) are lowercase                                     | transform to uppercase              |

