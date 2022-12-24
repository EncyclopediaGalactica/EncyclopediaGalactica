# Sdk Generator

The purpose of the Sdk Generator is that based on the endpoint OpenApi specification it
generates the Sdk Client. It includes the following:

- SDK interface with documentation
- SDK client source code with documentation
- operations for operations defined in the OpenApi specification like:
  - add
  - delete
  - update
- unit tests for SDK client code
- integration tests for SDK client code with mocks, and it is super extensive tests
- SDK Models
- SDK models unit tests

## How it works?

- execute the command line in the directory you would like to have the code
  - appends to a solution
  - define solution directory
  - define real directory
- define where the OpenApi spec is available
- generates client
  - if no client project creates one
  - if there is a client it cleans up first and add them again
- generates client unit tests
- generates client int tests
- generates models
- generates models unit test
- builds solution