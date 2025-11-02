from rich.table import Table
from rich.console import Console
import typer
from typing import Annotated
from scenarios.storage.edge_types.add import (
    add_edge_type_scenario,
    AddEdgeTypeScenarioInput,
)

app = typer.Typer(no_args_is_help=True, rich_markup_mode="markdown")


@app.command()
def add(
    name: Annotated[str, typer.Option(help="Name of the edge type")],
    description: Annotated[str, typer.Option(help="Description of the edge type")],
):
    """
    Adds a new edge type to the storage.
    """
    input = AddEdgeTypeScenarioInput.from_cli(name=name, description=description)
    result = add_edge_type_scenario(input)
    console = Console()
    table = Table(
        "Id",
        "Name",
        "Description",
        title="Adding an edge type",
        show_header=True,
        header_style="bold magenta",
    )
    table.add_row(str(result.id), str(result.name), str(result.description))
    console.print(table)
