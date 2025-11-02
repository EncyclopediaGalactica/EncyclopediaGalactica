from dataclasses import dataclass
from storage.edges.list import list_edges
from storage.db import get_session
from typing import List
from storage.edges.entity import Edge


def list_edges_scenario() -> List[ListEdgesScenarioResult]:
    with next(get_session()) as session:
        result = list_edges(session)
        return [ListEdgesScenarioResult.from_entity(edge) for edge in result]


@dataclass
class ListEdgesScenarioResult:
    id: int
    from_vertex_id: int
    to_vertex_id: int

    @classmethod
    def from_entity(cls, entity: Edge) -> "ListEdgesScenarioResult":
        return cls(
            id=entity.id,
            from_vertex_id=entity.from_vertex_id,
            to_vertex_id=entity.to_vertex_id,
        )
