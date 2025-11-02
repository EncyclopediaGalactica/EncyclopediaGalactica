import typer
from cli.storage import vertex
from cli.storage import edge_types
from cli.storage import edges

app = typer.Typer(no_args_is_help=True, rich_markup_mode="markdown")


@app.callback()
def storage():
    """
    Storage management related commands.
    """


app.add_typer(vertex.app, name="vertex")
app.add_typer(edge_types.app, name="edge_types")
app.add_typer(edges.app, name="edges")
