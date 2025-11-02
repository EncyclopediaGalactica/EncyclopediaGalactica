from sqlalchemy.orm import Session
from storage.edge_types.entity import EdgeType
from typing import List


def get_edge_types(session: Session) -> List[EdgeType]:
    return session.query(EdgeType).all()
