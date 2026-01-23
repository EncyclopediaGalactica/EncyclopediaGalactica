import typer
from cli.storage.vertex import vertex

app = typer.Typer(no_args_is_help=True)

@app.callback()
def storage():
    """
    Storage management related commands.
    """

app.add_typer(vertex.app, name="vertex")
