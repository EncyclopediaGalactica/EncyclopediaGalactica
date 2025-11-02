from dataclasses import dataclass
from storage.edge_types.entity import EdgeType
from scenarios.storage.storage import ValidationResult, StorageScenarioError
from storage.db import get_session
from storage.edge_types.add import add_edge_type


def add_edge_type_scenario(
    input: AddEdgeTypeScenarioInput,
) -> AddEdgeTypeScenarioResult:
    validation_result = validate_input(input)
    if validation_result.is_valid is False:
        raise StorageScenarioError(
            validation_result.get_error_message(),
            validation_result.get_error_type(),
            validation_result.get_error_details(),
        )
    edge_type_entity = input.to_entity()
    with next(get_session()) as session:
        result = add_edge_type(edge_type_entity, session)
        return AddEdgeTypeScenarioResult.from_entity(result)


def validate_input(input: AddEdgeTypeScenarioInput) -> ValidationResult:
    validation_result = ValidationResult()
    if input.name == "":
        validation_result.validation_failed()
        validation_result.add_error_detail("Name must not be empty")
    if len(input.name.strip()) < 3:
        validation_result.validation_failed()
        validation_result.add_error_detail("Name must be at least 3 characters long")
    if input.description == "":
        validation_result.validation_failed()
        validation_result.add_error_detail("Description must not be empty")
    if len(input.description.strip()) < 3:
        validation_result.validation_failed()
        validation_result.add_error_detail(
            "Description must be at least 3 characters long"
        )
    return validation_result


@dataclass
class AddEdgeTypeScenarioInput:
    name: str
    description: str

    @classmethod
    def from_cli(cls, name: str, description: str):
        return cls(name=name, description=description)

    def to_entity(self) -> EdgeType:
        return EdgeType(name=self.name, description=self.description)


@dataclass
class AddEdgeTypeScenarioResult:
    id: int
    name: str
    description: str

    @classmethod
    def from_entity(cls, entity: EdgeType) -> "AddEdgeTypeScenarioResult":
        return cls(id=entity.id, name=entity.name, description=entity.description)
