from rich.table import Table
from rich.console import Console
import typer
from scenarios.storage.vertices.list_vertices import list_vertices

app = typer.Typer(no_args_is_help=True, rich_markup_mode="markdown")


@app.command()
def list():
    """
    Retrieves the full list of vertices from the storage.
    """
    vertices = list_vertices()
    console = Console()
    result = Table(
        "Id",
        "Name",
        "Description",
        "Data",
        title="Listing vertices",
        show_header=True,
        header_style="bold magenta",
    )
    for vertex in vertices:
        result.add_row(
            str(vertex.id), str(vertex.name), str(vertex.description), str(vertex.data)
        )
    console.print(result)
