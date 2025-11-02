from sqlalchemy.orm import Session
from storage.edge_types.entity import EdgeType


def add_edge_type(input: EdgeType, session: Session) -> EdgeType:
    session.begin()
    session.add(input)
    session.commit()
    return input
