Feature: Yes/No Field Validation
  As a compliance tester
  I want to ensure Yes/No fields contain valid values
  So that invalid data is caught early

   @tag2
  Scenario: Validate Yes/No fields in the report
    Given the report files and Excel definitions are available
    When I run the Yes No field validation
    Then there should be no invalid Yes No values


@ignore
 Scenario Outline: Validate Yes/No Field Values - Example for Dynamic Data Handling (Parameterized Testing)
  Given the input file "<filename>" and expected column "<columnCode>"
  When I validate the column values
  Then all values should be either "Yes" or "No"

  Examples:
    | filename      | columnCode |
    | y_01.01.csv   | c1         |
    | y_02.01.csv   | c2         |
