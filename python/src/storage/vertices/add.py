from sqlalchemy.orm import Session
from storage.vertices.entity import Vertex


def add(input: Vertex, session: Session) -> Vertex:
    session.begin()
    session.add(input)
    session.commit()
    return input
