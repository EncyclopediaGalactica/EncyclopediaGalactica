import typer

app = typer.Typer(no_args_is_help=True, rich_markup_mode="markdown")


@app.command()
def update():
    """
    Updates the designated edge type in the storage.
    """
    print("update")
