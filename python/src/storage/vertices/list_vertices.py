from sqlalchemy.orm import Session
from storage.vertices.entity import Vertex
from typing import List


def list_vertices(session: Session) -> List[Vertex]:
    return session.query(Vertex).all()
