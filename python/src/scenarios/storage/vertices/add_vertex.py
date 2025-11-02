from dataclasses import dataclass
from storage.db import get_session
from storage.vertices.add import add
from storage.vertices.entity import Vertex
from scenarios.storage.storage import ValidationResult, StorageScenarioError


def add_vertex(input: AddVertexScenarioInput) -> AddVertexScenarioResult:
    """
    Adds a new vertex to the storage.
    """
    validation_result = validate_input(input)
    if validation_result.is_valid is False:
        raise StorageScenarioError(
            validation_result.get_error_message(),
            validation_result.get_error_type(),
            validation_result.get_error_details(),
        )
    data = '{{ "data": {{ "{0}": "{1}" }} }}'.format(input.property, input.data)
    input.data = data
    vertex = input.to_entity()

    with next(get_session()) as session:
        created_entity = add(vertex, session)
        result = AddVertexScenarioResult.from_entity(created_entity)
        return result


def validate_input(input: AddVertexScenarioInput) -> ValidationResult:
    validation_result = ValidationResult()
    if input.name == "" or input.name == " ":
        validation_result.add_error_detail("name is empty")

    if len(input.name.strip()) < 3:
        validation_result.add_error_detail(
            "name trimmed length must be longer than 3 characters"
        )

    if input.description == "" or input.description == " ":
        validation_result.add_error_detail("description is empty")

    if len(input.description.strip()) < 3:
        validation_result.add_error_detail(
            "description trimmed length must be longer than 3 characters"
        )

    return validation_result


@dataclass
class AddVertexScenarioInput:
    name: str
    description: str
    property: str
    data: str

    @classmethod
    def from_cli_input(
        cls, name: str, description: str, property: str, data: str
    ) -> "AddVertexScenarioInput":
        return cls(name=name, description=description, property=property, data=data)

    def to_entity(self) -> Vertex:
        return Vertex(name=self.name, description=self.description, data=self.data)


@dataclass
class AddVertexScenarioResult:
    id: int
    name: str
    description: str
    data: str

    @classmethod
    def from_entity(cls, entity: Vertex) -> "AddVertexScenarioResult":
        return cls(entity.id, entity.name, entity.description, entity.data)
