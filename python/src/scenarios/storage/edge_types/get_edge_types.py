from dataclasses import dataclass
from storage.db import get_session
from storage.edge_types.get_edge_types import get_edge_types
from storage.edge_types.entity import EdgeType
from typing import List


def get_edge_types_scenario() -> List[GetEdgeTypesScenarioResult]:
    with next(get_session()) as session:
        edge_types = get_edge_types(session)
        return [
            GetEdgeTypesScenarioResult.from_entity(edge_type)
            for edge_type in edge_types
        ]


@dataclass
class GetEdgeTypesScenarioResult:
    id: int
    name: str
    description: str

    @classmethod
    def from_entity(cls, entity: EdgeType) -> "GetEdgeTypesScenarioResult":
        return cls(id=entity.id, name=entity.name, description=entity.description)
