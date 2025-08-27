import typer

from scenarios.storage.graph.list_vertices import list_vertices

app = typer.Typer(no_args_is_help=True)

@app.callback()
def vertex():
    """
    Managing vertices in the storage.
    """

@app.command()
def add():
    """
    Adds a new vertex to the storage.
    """
    print("add")


@app.command()
def list():
    """
    Retrieves the full list of vertices from the storage.
    """
    list_vertices()


@app.command()
def delete():
    print("delete")


@app.command()
def update():
    print("update")
