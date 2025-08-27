import typer

from cli.calculate import calculate
from cli.storage import storage

app = typer.Typer(no_args_is_help=True)

@app.callback()
def cli():
    """
    A CLI interface to work with all the knowledge in the known Universe.
    """

app.add_typer(storage.app, name="storage")
app.add_typer(calculate.app, name="calculate")
