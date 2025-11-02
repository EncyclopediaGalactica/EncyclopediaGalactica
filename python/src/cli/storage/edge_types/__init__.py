import typer
from .add import app as add_app
from .list import app as list_app
from .delete import app as delete_app
from .update import app as update_app

app = typer.Typer(no_args_is_help=True, rich_markup_mode="markdown")
app.add_typer(add_app, name=None)
app.add_typer(list_app, name=None)
app.add_typer(delete_app, name=None)
app.add_typer(update_app, name=None)


@app.callback()
def edge_types():
    """
    Managing edge types in the storage.
    """
