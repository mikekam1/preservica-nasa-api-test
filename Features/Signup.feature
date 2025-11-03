@UI
Feature: NASA API Signup

  Scenario: Verify signup form is accessible
    Given I am on the NASA API signup page
    When I check the page
    Then I should see the signup form
