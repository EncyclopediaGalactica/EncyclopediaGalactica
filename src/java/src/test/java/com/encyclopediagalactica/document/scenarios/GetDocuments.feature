Feature: Get the list of documents

  The Get the list of documents feature is about listing what documents
  the system has independently from if these documents are active or
  inactive.

  Scenario Template: Getting the list of documents

    Given we have <amount> documents in the system already
    When the list of documents are requested
    Then the result list length is <resultAmount>

    Examples:
      | amount | resultAmount |
      | 0      | 0            |
      | 1      | 1            |
      | 2      | 2            |
