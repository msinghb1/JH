Feature: Duplicate Column Check
  As a test validator
  I want to ensure no report contains duplicate columns
  So that we meet regulatory data standards

  @tag1 
  Scenario: Validate no duplicate columns exist in any report
    Given the report files are loaded
    When I run the duplicate column validation
    Then there should be no duplicate columns
