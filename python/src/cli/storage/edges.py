import typer
from typing import Annotated
from scenarios.storage.edges.list import list_edges_scenario

app = typer.Typer(no_args_is_help=True, rich_markup_mode="markdown")


@app.callback()
def edges():
    """
    Managing edges in the storage.
    """


@app.command()
def add(
    edge_from: Annotated[int, typer.Option(help="Edge FROM")],
    edge_to: Annotated[int, typer.Option(help="Edge TO")],
):
    """
    Adds edge between two vertices
    """
    print("add")


@app.command()
def list():
    """
    List of edges
    """
    result = list_edges_scenario()
    print(result)


@app.command()
def delete():
    """
    Deletes the designated edge from the storage
    """
    print("delete")


@app.command()
def update():
    """
    Updates the designated edge in the storage
    """
    print("update")
