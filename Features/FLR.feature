@API
Feature: FLR API Testing

  Scenario: Get solar flare data with valid parameters
    Given I have the FLR endpoint
    When I request FLR data from "2023-01-01" to "2023-01-07"
    Then the FLR response status should be 200
    And the FLR response should be a valid JSON array

  Scenario: Request without required startDate parameter
    Given I have the FLR endpoint
    When I request data without startDate
    Then the FLR response status should be 400
