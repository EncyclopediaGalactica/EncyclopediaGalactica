from dataclasses import dataclass
from storage.db import get_session
from typing import List
from storage.vertices.entity import Vertex


def list_vertices() -> List[ListVerticesScenarioResult]:
    with next(get_session()) as session:
        return [
            ListVerticesScenarioResult.from_entity(vertex)
            for vertex in session.query(Vertex).all()
        ]


@dataclass
class ListVerticesScenarioResult:
    id: int
    name: str
    description: str
    data: str

    @classmethod
    def from_entity(cls, entity: Vertex) -> "ListVerticesScenarioResult":
        return cls(entity.id, entity.name, entity.description, entity.data)
