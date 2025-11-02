from rich.table import Table
from rich.console import Console
import typer
from scenarios.storage.edge_types.get_edge_types import get_edge_types_scenario

app = typer.Typer(no_args_is_help=True, rich_markup_mode="markdown")


@app.command()
def list():
    """
    Retrieves the full list of edge types from the storage.
    """
    result = get_edge_types_scenario()
    console = Console()
    table = Table(
        "Id",
        "Name",
        "Description",
        title="Listing edge types",
        show_header=True,
        header_style="bold magenta",
    )
    for edge_type in result:
        table.add_row(
            str(edge_type.id), str(edge_type.name), str(edge_type.description)
        )
    console.print(table)
