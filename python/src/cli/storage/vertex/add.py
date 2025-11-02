from typing import Annotated
import typer
from rich.console import Console
from rich.table import Table
from scenarios.storage.vertices.add_vertex import add_vertex, AddVertexScenarioInput

app = typer.Typer(no_args_is_help=True, rich_markup_mode="markdown")


@app.command()
def add(
    name: Annotated[str, typer.Option(help="Name of the vertex")],
    description: Annotated[str, typer.Option(help="Description of the vertex")],
    property: Annotated[
        str,
        typer.Option(
            help="The property where to the data is going to be added. See --help",
        ),
    ] = "",
    data: Annotated[
        str,
        typer.Option(
            help="The text based data. See --help",
        ),
    ] = "",
):
    """
    Adds a new vertex to the storage.

    When adding `--property` and `--data` to the vertex, the data going to be be part of a

    JSON object like the following where the `{data}` represents the input

    you provide:

    ```json
        {
            "data": {
                "property": "data"
            }
        }
    ```

    In the input you have to provide only the node name and its content like below:

    ```bash
        eg storage vertex add --property "prop_name" --data "prop value"
    ```

    and it will be:

    ```json
        {
            "data": {
                "prop_name": "prop value"
                }
        }
    ```
    """
    input = AddVertexScenarioInput.from_cli_input(
        name=name, description=description, property=property, data=data
    )
    console = Console()
    result = add_vertex(input)
    table = Table(
        "Id",
        "Name",
        "Description",
        "Data",
        title="Adding a Vertex operation result",
        show_header=True,
        header_style="bold magenta",
    )
    table.add_row(
        str(result.id), str(result.name), str(result.description), str(result.data)
    )
    console.print(table)
