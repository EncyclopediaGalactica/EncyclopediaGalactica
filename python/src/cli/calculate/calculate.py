import typer

app = typer.Typer(no_args_is_help=True)


@app.callback()
def calculate():
    """
    Some magic calculations.
    """
