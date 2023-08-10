<!-- TOC -->

* [Code Generator Specification based on Configuration file](#code-generator-specification-based-on-configuration-file)
    * [The generator internal data structure](#the-generator-internal-data-structure)
        * [SolutionInfo](#solutioninfo)
        * [ProjectInfo](#projectinfo)
        * [TypeInfo](#typeinfo)
        * [VariableInfo](#variableinfo)
    * [c-sharp](#c-sharp)
        * [Solution level rules](#solution-level-rules)
        * [Dto Project level configurations](#dto-project-level-configurations)
        * [Dto Unit Test Project](#dto-unit-test-project)
* [Validation process](#validation-process)
* [Configuration via json file](#configuration-via-json-file)
    * [$schema](#schema)
    * [lang](#lang)
    * [OpenApi Specification Path](#openapi-specification-path)
    * [Target directory](#target-directory)
    * [Solution Name](#solution-name)
    * [Solution Base Namespace](#solution-base-namespace)
    * [Solution file type](#solution-file-type)
        * [C# specifics](#c-specifics)

<!-- TOC -->

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

- **Solution Name** - the name of the solution and this value defines the name of the solution
  file. This value is specified like: `solution_name`.
- **Target directory** - the directory where the solution is located. This value can be
  specified
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

# Validation process

The generator applies validation in the following order:

- input validation of the configuration values
- target directory structure validation

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

| Available generators | lang code |
|----------------------|-----------|
| C#                   | c-sharp   |

| Input        | Result          |
|--------------|-----------------|
| valid input  | Code Generation |
| string.Empty | Error           |
| spaces       | Error           |
| unknown lang | Error           |

## OpenApi Specification Path

```json
{
  "openapi_specification_path": "absolute or relative path to the openapi.yaml file"
}
```

The generator uses the OpenApi file to generate majority of the codebase.

**Input validation rules**

| Input        | Result |
|--------------|--------|
| null         | throw  |
| string.empty | throw  |
| whitespace   | throw  |

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

**Input validation rules**

| Input        | Result |
|--------------|--------|
| null         | throw  |
| string.empty | throw  |
| whitespace   | throw  |

## Solution Name

```json
{
  "solution_name": "SolutionName"
}
```

The name of the solution. The code generator will look for a file with this name with the
configured file type.

**Input validation rules**

| Input                                          | Result |
|------------------------------------------------|--------|
| null                                           | throws |
| string.empty                                   | throws |
| whitespace                                     | throws |
| first char is a number                         | throws |
| contains special characters other than dot (.) | throws |

## Solution Base Namespace

```json
{
  "solution_base_namespace": "something.namespace"
}
```

The solution level base namespace is used to build other namespaces during generation.

**Validation and transformation rules**

| Input                 | Result                |
|-----------------------|-----------------------|
| "something.namespace" | "Something.Namespace" |
| "something."          | "Something"           |
| ".something"          | "Something"           |
| null                  | throw                 |
| "  "                  | throw                 |

## Solution file type

The solution file type is used by the generator to build paths to the solution files.

**Validation rules**

| Input                          | Result |
|--------------------------------|--------|
| null                           | throws |
| string.empty                   | throws |
| "  "                           | throws |
| contains anything than letters | throws |

### C# specifics

