from typing import List
from storage.edges.entity import Edge
from sqlalchemy.orm import Session


def list_edges(session: Session) -> List[Edge]:
    return session.query(Edge).all()
