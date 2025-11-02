from typing import List


class StorageScenarioError(Exception):
    def __init__(self, message: str, error_type: str, error_details: str):
        super().__init__(message)
        self.error_type = error_type
        self.error_details = error_details


class ValidationResult:
    def __init__(self):
        self.is_valid = True
        self.error_message = ""
        self.error_type = ""
        self.error_details = []

    is_valid: bool
    error_message: str
    error_type: str
    error_details: List[str]

    def add_error_detail(self, error_text: str):
        self.error_details.append(error_text)

    def validation_failed(self):
        self.is_valid = False

    def set_error_type(self, error_type: str):
        self.error_type = error_type

    def get_error_type(self) -> str:
        return self.error_type

    def set_error_message(self, error_message: str):
        self.error_message = error_message

    def get_error_message(self) -> str:
        return self.error_message

    def get_error_details(self) -> str:
        return "\n".join(self.error_details)
