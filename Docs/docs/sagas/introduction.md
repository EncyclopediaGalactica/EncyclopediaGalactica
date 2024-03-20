# Introduction

A saga describe a process consisting multiple steps.
A saga also includes compensation actions if any of the steps fails.

A saga is responsible for a business process controlled execution.
It also includes the cases when the process cannot be executed and some state must be created 
which can be either the starting state or something else.
The saga's responsibility to orchestrate execution in a way to achieve this state.

> [!Note]
> In the early phase of development we are not going to identify different users or user groups.
> It would make the development process and thinking more complex than we need now.
> At this moment we are going to list functionalities and in a later phase these will be assigned 
> to possible groups.
