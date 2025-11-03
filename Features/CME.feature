@API
Feature: CME API Testing

  Scenario: Get CME data with valid date range
    Given I have the CME endpoint
    When I request CME data from "2023-01-01" to "2023-01-07"
    Then the CME response status should be 200
    And the CME response should be a valid JSON array

  Scenario: Request CME data with invalid date format
    Given I have the CME endpoint
    When I request data with invalid date "invalid-date"
    Then the CME response status should be 400
